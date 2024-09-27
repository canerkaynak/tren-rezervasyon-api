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
             * Each car iterated
             * If there are empty seats and reserved seats are below the 70% of total capacity, other criterieas review
             * If everyone wants to seat in the same car, considered whether there are enough available seats.
             * If different cars is not a problem, available seats are arranged
             * These arrangement added to details list
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
