using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlightsSystem.DBGenerator.Models;
using Newtonsoft.Json.Linq;

namespace FlightsSystem.DBGenerator.Services
{
    public class RandomUserService
    {
        private const string apiUrl = "https://randomuser.me/api";

        public async Task<User> GetRandomUser()
        {
            var json = JObject.Parse(await new HttpClient()
                .GetStringAsync(apiUrl));
            return ParseUser(json["results"].First);
        }

        private User ParseUser(dynamic json)
        {
            return new User()
            {
                Name = $"{json.name.title} " +
                       $"{json.name.first} " +
                       $"{json.name.last}",
                FirstName = $"{json.name.first}",
                LastName = $"{json.name.last}",
                Email = $"{json.email}",
                Username = $"{json.login.username}",
                Password = $"{json.login.password}",
                Country = $"{json.location.country}",
                Gender = $"{json.gender}",
                Phone = $"{json.phone}",
                PictureUrl = $"{json.picture.thumbnail}",
                Address =
                    $"{json.location.state}," +
                    $"{json.location.city}," +
                    $"{json.location.street.name}" +
                    $"{json.location.street.number}"
            };
        }
    }

    // json user example
    /*
     *{
    "results": [{
        "gender": "female",
        "name": {
            "title": "Ms",
            "first": "Mia",
            "last": "Chen"
        },
        "location": {
            "street": {
                "number": 7600,
                "name": "Riverside Drive"
            },
            "city": "Lower Hutt",
            "state": "Northland",
            "country": "New Zealand",
            "postcode": 34117,
            "coordinates": {
                "latitude": "59.0734",
                "longitude": "23.0229"
            },
            "timezone": {
                "offset": "-1:00",
                "description": "Azores, Cape Verde Islands"
            }
        },
        "email": "mia.chen@example.com",
        "login": {
            "uuid": "a0e6cc68-4d4d-4892-b2b1-4f7d59d22685",
            "username": "bigelephant981",
            "password": "popper",
            "salt": "2q2ILj8i",
            "md5": "f30583385c1fa0a333dccc2f1e5fb8d9",
            "sha1": "c175beb476c5a22ab318b7e9d98ec2512b005392",
            "sha256": "c885d3edf83c73fc47390bf4b72fd36e3cab58dae098ae623071dbc9b9e4e37b"
        },
        "dob": {
            "date": "1969-06-03T15:06:22.194Z",
            "age": 51
        },
        "registered": {
            "date": "2016-09-30T14:10:38.043Z",
            "age": 4
        },
        "phone": "(581)-135-3817",
        "cell": "(026)-589-4458",
        "id": {
            "name": "",
            "value": null
        },
        "picture": {
            "large": "https://randomuser.me/api/portraits/women/34.jpg",
            "medium": "https://randomuser.me/api/portraits/med/women/34.jpg",
            "thumbnail": "https://randomuser.me/api/portraits/thumb/women/34.jpg"
        },
        "nat": "NZ"
    }],
    "info": {
        "seed": "a9da967b789eca85",
        "results": 1,
        "page": 1,
        "version": "1.3"
    }
}
     *
     *
     *
     */
}