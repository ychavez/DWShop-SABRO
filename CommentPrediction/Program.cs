namespace CommentPrediction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Console.WriteLine("type your comment");
                //Load sample data
                var sampleData = new MLModel1.ModelInput()
                {
                    Col0 = Console.ReadLine(),
                };

                //Load model and predict output
                var result = MLModel1.Predict(sampleData);

                Console.WriteLine(result.PredictedLabel);
                Console.Read();
            }
        }
    }
}
