using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartStore.Core;
using SmartStore.Core.Caching;
using SmartStore.Services.DellyMan;
using SmartStore.Services.Directory;
using SmartStore.Services.Localization;
using SmartStore.Web.Framework.Controllers;

namespace SmartStore.DellyManLogistics.Controllers
{
    public partial class CountryController : PublicControllerBase
    {
        #region Fields

        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly IDellyManService _dellyManService;

        #endregion

        #region Constructors

        public CountryController(ICountryService countryService,
            IStateProvinceService stateProvinceService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            ICacheManager cacheManager,
            IDellyManService dellyManService)
        {
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._cacheManager = cacheManager;
            _dellyManService = dellyManService;
        }

        #endregion

        #region States / provinces

        /// <summary>
        /// This action method gets called via an ajax request.
        /// </summary>
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> GetStatesByCountryId(string countryId, bool addEmptyStateIfRequired)
        {
            if (!countryId.HasValue())
                throw new ArgumentNullException("countryId");

            if (!countryId.HasValue())
                return Json(new { id = 0, name = "" }, JsonRequestBehavior.AllowGet);

            var stateProvince = _stateProvinceService.GetStateProvinceById(int.Parse(countryId));
            var states = await _dellyManService.GetStatesAsync();
            var selectedState = states.FirstOrDefault(s => s.Name == stateProvince.Name);

            var cities = await _dellyManService.GetCitiesAsync(selectedState.StateID);

            var data = cities.Select(c => new { id = c.CityID, name = c.Name }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

       
        #endregion
    }
}