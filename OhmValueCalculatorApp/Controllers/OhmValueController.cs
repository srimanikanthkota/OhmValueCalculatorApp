using OhmValueCalculatorApp.BusinessLayer;
using OhmValueCalculatorApp.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OhmValueCalculatorApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OhmValueController : ApiController
    {
        public IOhmValueCalculator OhmValueCalculator { get; set; }
        public OhmValueController()
        {
            OhmValueCalculator = new OhmValueCalculator();
        }

        [HttpGet]
        [Route("OhmValue")]
        public IHttpActionResult IAmAlive()
        {
            return Ok(new { ResultCode = 0, ResultMessage = "Success: The ohm value api is running..." });
        }

        [HttpGet]
        [Route("OhmValue/Band4/ResistorColorCodes")]
        public IHttpActionResult GetResitorColorCodes()
        {
            var result = OhmValueCalculator.GetResitorColorCodeList();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("OhmValue/Band4/Calculate")]
        public IHttpActionResult Calculate(OhmBandInputModel ohmBand)
        {
            try
            {
                //Get ohm value for given band 4 color codes
                var ohmValue = OhmValueCalculator.CalculateOhmValue(bandAColor: ohmBand.ColorA, bandBColor: ohmBand.ColorB, bandCColor: ohmBand.ColorC, bandDColor: ohmBand.ColorD);

                //Get tolerance value for display purpose
                var tolerance = OhmValueCalculator.GetToleranceValue(bandDColor: ohmBand.ColorD);

                //Get dinominator Letter value for display purpose
                var ohmDenominatorLetter = OhmValueCalculator.GetMultiplierDenominatorLetter(multiplierBandColor: ohmBand.ColorC);
                var ohmDenominatorDivisor = OhmValueCalculator.GetOhmValueDivisor(multiplierBandColor: ohmBand.ColorC);

                return Ok(new OhmResultModel
                {
                    ResultCode = 0,
                    ResultMessage = "Success",
                    OhmValue = ohmValue,

                    ToleranceValue = tolerance,
                    OhmDenominatedLetter = ohmDenominatorLetter,
                    OhmDenominatedValue = ohmDenominatorDivisor != null ? ohmValue / ohmDenominatorDivisor : ohmValue
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
