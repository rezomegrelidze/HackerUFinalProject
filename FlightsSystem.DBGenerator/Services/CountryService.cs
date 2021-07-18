using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlightsSystem.Core;
using Newtonsoft.Json.Linq;

namespace FlightsSystem.DBGenerator.Services
{
    public class CountryService
    {
        private const string apiUrl = "https://restcountries.eu/rest/v2";

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var jsonArray = JArray.Parse(await new HttpClient().GetStringAsync(apiUrl));
            return jsonArray
                .Select(country => new Country() {CountryName = country["name"].ToString()});
        }

        public async Task<string> GetCountryFlag(string countryName)
        {
            var jsonArray = JArray.Parse(await new HttpClient().GetStringAsync(apiUrl));
            var country = jsonArray.SingleOrDefault(c => c["name"].ToString().ToLower() == countryName.ToLower());
			if(country == null) throw new InvalidOperationException($"Country {countryName} doesn't exist");
            return country["flag"].ToString();
        }

        public async Task<string> GetCountryAlpha2Code(string countryName)
        {
            var jsonArray = JArray.Parse(await new HttpClient().GetStringAsync(apiUrl));
            var country = jsonArray.SingleOrDefault(c => c["name"].ToString().ToLower() == countryName.ToLower());
            if (country == null) throw new InvalidOperationException($"Country {countryName} doesn't exist");
            return country["alpha2Code"].ToString();
        }
	}
	// country json object example:
	/*
     *{
	"name": "Afghanistan",
	"topLevelDomain": [".af"],
	"alpha2Code": "AF",
	"alpha3Code": "AFG",
	"callingCodes": ["93"],
	"capital": "Kabul",
	"altSpellings": ["AF", "Afġānistān"],
	"region": "Asia",
	"subregion": "Southern Asia",
	"population": 27657145,
	"latlng": [33.0, 65.0],
	"demonym": "Afghan",
	"area": 652230.0,
	"gini": 27.8,
	"timezones": ["UTC+04:30"],
	"borders": ["IRN", "PAK", "TKM", "UZB", "TJK", "CHN"],
	"nativeName": "افغانستان",
	"numericCode": "004",
	"currencies": [{
		"code": "AFN",
		"name": "Afghan afghani",
		"symbol": "؋"
	}],
	"languages": [{
		"iso639_1": "ps",
		"iso639_2": "pus",
		"name": "Pashto",
		"nativeName": "پښتو"
	}, {
		"iso639_1": "uz",
		"iso639_2": "uzb",
		"name": "Uzbek",
		"nativeName": "Oʻzbek"
	}, {
		"iso639_1": "tk",
		"iso639_2": "tuk",
		"name": "Turkmen",
		"nativeName": "Türkmen"
	}],
	"translations": {
		"de": "Afghanistan",
		"es": "Afganistán",
		"fr": "Afghanistan",
		"ja": "アフガニスタン",
		"it": "Afghanistan",
		"br": "Afeganistão",
		"pt": "Afeganistão",
		"nl": "Afghanistan",
		"hr": "Afganistan",
		"fa": "افغانستان"
	},
	"flag": "https://restcountries.eu/data/afg.svg",
	"regionalBlocs": [{
		"acronym": "SAARC",
		"name": "South Asian Association for Regional Cooperation",
		"otherAcronyms": [],
		"otherNames": []
	}],
	"cioc": "AFG"
}
     *
     */
}