using Microsoft.AspNetCore.Mvc;
using TrainReservationApi.Models;

namespace TrainReservationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        [HttpPost]
        public ActionResult<RezervasyonResponse> MakeReservation([FromBody] RezervasyonRequest request)
        {
            var response = new RezervasyonResponse();
            response.YerlesimAyrinti = new List<YerlesimAyrinti>();

            int istenilenRezervasyon = request.RezervasyonYapilacakKisiSayisi;
            int toplamYapılmışRezervasyon = 0;

            /*
             * Vagonlar uzerinde dongu donuldu.
             * Vagon kapasitesinin yuzde yetmisi uzerinden uygun koltuk sayisi hesaplandi.
             * Rezerve edilecek koltuk sayisi farkli vagonlara,
             * yerlestirilip yerlestirilemeyecegi kosuluna gore belirlendi
             * 
             * Time and Space complexity are O(n)
            */
            foreach (var vagon in request.Tren.Vagonlar)
            {
                int uygunKoltuklar = (vagon.Kapasite * 7 / 10) - vagon.DoluKoltukAdet;

                if (uygunKoltuklar > 0)
                {
                    int rezerveEdilecekKoltuklar = request.KisilerFarkliVagonlaraYerlestirilebilir ? ((istenilenRezervasyon - toplamYapılmışRezervasyon) <= uygunKoltuklar ? istenilenRezervasyon - toplamYapılmışRezervasyon : uygunKoltuklar - toplamYapılmışRezervasyon) : (istenilenRezervasyon <= uygunKoltuklar ? istenilenRezervasyon : 0);

                    if (rezerveEdilecekKoltuklar > 0)
                    {
                        response.YerlesimAyrinti.Add(new YerlesimAyrinti { VagonAdi = vagon.Ad, KisiSayisi = rezerveEdilecekKoltuklar});
                        toplamYapılmışRezervasyon += rezerveEdilecekKoltuklar;
                    }

                    if (toplamYapılmışRezervasyon >= istenilenRezervasyon)
                        break;
                }
            }

            response.RezervasyonYapilabilir = toplamYapılmışRezervasyon >= istenilenRezervasyon;

            if (!response.RezervasyonYapilabilir)
            {
                response.YerlesimAyrinti.Clear();
            }

            return Ok(response);
        }
    }
}
