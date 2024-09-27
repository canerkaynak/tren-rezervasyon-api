namespace TrainReservationApi.Models
{
    public class RezervasyonRequest
    {
        public required Tren Tren { get; set; } = new Tren();
        public required int RezervasyonYapilacakKisiSayisi { get; set; }
        public required bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}
