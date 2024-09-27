namespace TrainReservationApi.Models
{
    public class Vagon
    {
        public required string Ad { get; set; } = string.Empty;
        public required int Kapasite { get; set; } = 0;
        public required int DoluKoltukAdet { get; set; } = 0;
    }
}
