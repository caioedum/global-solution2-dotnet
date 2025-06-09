using Microsoft.ML;
using Microsoft.ML.Data;

namespace HelperDrone.Models
{
    public class SentimentAnalysis
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public SentimentAnalysis()
        {
            _mlContext = new MLContext();
        }

        public class SentimentData
        {
            [LoadColumn(0)] public string Text { get; set; }
            [LoadColumn(1)] public bool Label { get; set; }
        }

        public class SentimentPrediction : SentimentData
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }

            public float Probability { get; set; }
        }

        public void TrainModel()
        {
            string dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "sentiment_data.csv");

            if (!File.Exists(dataPath))
            {
                throw new FileNotFoundException("Arquivo não encontrado.", dataPath);
            }

            IDataView dataView = _mlContext.Data.LoadFromTextFile<SentimentData>(
                dataPath,
                separatorChar: ',',
                hasHeader: true
            );

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            _model = pipeline.Fit(dataView);
            _mlContext.Model.Save(_model, dataView.Schema, "SentimentModel.zip");
        }

        public void LoadModel()
        {
            _model = _mlContext.Model.Load("SentimentModel.zip", out _);
        }

        public SentimentPrediction Predict(string text)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
            return predictionEngine.Predict(new SentimentData { Text = text });
        }
    }
}
