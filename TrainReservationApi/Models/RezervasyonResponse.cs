namespace TrainReservationApi.Models
{
    public class RezervasyonResponse
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; }
    }

    public class YerlesimAyrinti
    {
        public string VagonAdi { get; set; } = string.Empty;
        public int KisiSayisi { get; set; }
    }
}
