namespace TaskAsync.Tasks
{
    public class BreakFast
    {
        public void Run()
        {
            CalentarSarten();
            PrenderCafetera();
            CalentarHuevito();
            CalentarTocino();
            TostarPan();
            Servir();
        }

        public async Task RunAsync()
        {
            var taskCalentarSarten = CalentarSartenAsync();
            var cafeteraTask = PrenderCafeteraAsync();
            var panTask = TostarPanaAsync();


            await taskCalentarSarten;


            var huevitoTask = CalentarHuevitoAsync();
            var TocinoTask = CalentarTocinoAsync();



            await cafeteraTask;
            await panTask;
            await huevitoTask;
            await TocinoTask;
            await ServirAsync();

        }

        private static void Servir()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Servir");
        }

        private static void TostarPan()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Tostar Pan");
        }

        private static void CalentarTocino()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Calentar tocino");
        }

        private static void CalentarHuevito()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Calentar Huevito");
        }

        private static void PrenderCafetera()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Prender Cafetera");
        }

        private static void CalentarSarten()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Calentar Sarten");
        }






        private async static Task ServirAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Servir");
        }

        private async static Task TostarPanaAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Tostar Pan");
        }

        private async static Task CalentarTocinoAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Calentar tocino");
        }

        private async static Task CalentarHuevitoAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Calentar Huevito");
        }

        private async static Task PrenderCafeteraAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Prender Cafetera");
        }

        private async static Task CalentarSartenAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Calentar Sarten");
        }
    }
}
