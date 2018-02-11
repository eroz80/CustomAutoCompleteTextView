using System;
using System.Collections.Generic;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;

namespace AutoCompleteControl
{
    public class CountriesDatabase : SQLiteOpenHelper
    {
        // Database version
        public static readonly int DATABASE_VERSION = 1;

        // Database name
        public static readonly new string DatabaseName = "countriesdb";

        // TABLES
        public const string TableCountries = "countries";
        public const string FieldId = "_id";
        public const string FieldName = "name";
        private string _createTableCountries;


        public CountriesDatabase(Context context) : base(context, DatabaseName, null, DATABASE_VERSION)
        {
        }

        private void CreateTableCountriesString()
        {
            _createTableCountries =
                "CREATE TABLE " +
                "[" + TableCountries + "] " +
                "([" + FieldId + "] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
                "[" + FieldName + "] TEXT NOT NULL UNIQUE)";
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            CreateTableCountriesString();

            db.ExecSQL(_createTableCountries);

            foreach (var s in COUNTRIES)
            {
                InsertTableCountries(db, s);
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {

        }

        public void InitDatabase()
        {
            var cdb = WritableDatabase;
        }

        void InsertTableCountries(SQLiteDatabase db, string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && db != null)
            {

                var row = new ContentValues();
                row.Put(FieldName, name);
                db.Insert(TableCountries, null, row);

                /*
                db.ExecSQL(string.Format(
                    "INSERT INTO {0} ({1}) VALUES ('{2}')",
                    TableCountries, FieldName, name));
                    */
            }
        }

        public ICursor GetCountry(string id)
        {
            return ReadableDatabase.RawQuery(string.Format(
                "SELECT {0}, {1} FROM {2} WHERE {3} = {4}",
                FieldId, FieldName, TableCountries, FieldId, id), null);
        }

        public ICursor GetCountries()
        {
            return ReadableDatabase.RawQuery(string.Format(
                "SELECT * FROM {0}",
                TableCountries), null);
        }

        public ICursor GetCountries(string searchText)
        {
            return ReadableDatabase.RawQuery(string.Format(
                "SELECT * FROM {0} WHERE {1} LIKE '{2}%' ORDER BY {3} DESC LIMIT 0,5",
                TableCountries, FieldName, searchText, FieldId), null);
        }

        public ICursor GetCountries(string searchText, uint limit)
        {
            
            return ReadableDatabase.RawQuery(string.Format(
                "SELECT * FROM {0} WHERE {1} LIKE '{2}%' ORDER BY {3} LIMIT 0,{4}",
                TableCountries, FieldName, searchText, FieldName, limit), null);
        }

        private static string[] COUNTRIES = new String[] {
          "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra",
          "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina",
          "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
          "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
          "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia",
          "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory",
          "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
          "Cote d'Ivoire", "Cambodia", "Cameroon", "Canada", "Cape Verde",
          "Cayman Islands", "Central African Republic", "Chad", "Chile", "China",
          "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo",
          "Cook Islands", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic",
          "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica", "Dominican Republic",
          "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea",
          "Estonia", "Ethiopia", "Faeroe Islands", "Falkland Islands", "Fiji", "Finland",
          "Former Yugoslav Republic of Macedonia", "France", "French Guiana", "French Polynesia",
          "French Southern Territories", "Gabon", "Georgia", "Germany", "Ghana", "Gibraltar",
          "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau",
          "Guyana", "Haiti", "Heard Island and McDonald Islands", "Honduras", "Hong Kong", "Hungary",
          "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica",
          "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Laos",
          "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg",
          "Macau", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands",
          "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova",
          "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia",
          "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand",
          "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "North Korea", "Northern Marianas",
          "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
          "Philippines", "Pitcairn Islands", "Poland", "Portugal", "Puerto Rico", "Qatar",
          "Reunion", "Romania", "Russia", "Rwanda", "Sqo Tome and Principe", "Saint Helena",
          "Saint Kitts and Nevis", "Saint Lucia", "Saint Pierre and Miquelon",
          "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Saudi Arabia", "Senegal",
          "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
          "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "South Korea",
          "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden",
          "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "The Bahamas",
          "The Gambia", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey",
          "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Virgin Islands", "Uganda",
          "Ukraine", "United Arab Emirates", "United Kingdom",
          "United States", "United States Minor Outlying Islands", "Uruguay", "Uzbekistan",
          "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Wallis and Futuna", "Western Sahara",
          "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"
        };
    }
}

/*
Afghanistan
Albania
Algeria
Andorra
Angola
Antigua and Barbuda
Argentina
Armenia
Australia
Austria
Azerbaijan
Bahamas
Bahrain
Bangladesh
Barbados
Belarus
Belgium
Belize
Benin
Bhutan
Bolivia
Bosnia and Herzegovina
Botswana
Brazil
Brunei
Bulgaria
Burkina Faso
Burundi
Cabo Verde
Cambodia
Cameroon
Canada
Central African Republic(CAR)
Chad
Chile
China
Colombia
Comoros
Democratic Republic of the Congo
Republic of the Congo
Costa Rica
Cote d'Ivoire
Croatia
Cuba
Cyprus
Czech Republic
Denmark
Djibouti
Dominica
Dominican Republic
Ecuador
Egypt
El Salvador
Equatorial Guinea
Eritrea
Estonia
Ethiopia
Fiji
Finland
France
Gabon
Gambia
Georgia
Germany
Ghana
Greece
Grenada
Guatemala
Guinea
Guinea-Bissau
Guyana
Haiti
Honduras
Hungary
Iceland
India
Indonesia
Iran
Iraq
Ireland
Israel
Italy
Jamaica
Japan
Jordan
Kazakhstan
Kenya
Kiribati
Kosovo
Kuwait
Kyrgyzstan
Laos
Latvia
Lebanon
Lesotho
Liberia
Libya
Liechtenstein
Lithuania
Luxembourg
Macedonia(FYROM)
Madagascar
Malawi
Malaysia
Maldives
Mali
Malta
Marshall Islands
Mauritania
Mauritius
Mexico
Micronesia
Moldova
Monaco
Mongolia
Montenegro
Morocco
Mozambique
Myanmar(Burma)
Namibia
Nauru
Nepal
Netherlands
New Zealand
Nicaragua
Niger
Nigeria
North Korea
Norway
Oman
Pakistan
Palau
Palestine
Panama
Papua New Guinea
Paraguay
Peru
Philippines
Poland
Portugal
Qatar
Romania
Russia
Rwanda
Saint Kitts and Nevis
Saint Lucia
Saint Vincent and the Grenadines
Samoa
San Marino
Sao Tome and Principe
Saudi Arabia
Senegal
Serbia
Seychelles
Sierra Leone
Singapore
Slovakia
Slovenia
Solomon Islands
Somalia
South Africa
South Korea
South Sudan
Spain
Sri Lanka
Sudan
Suriname
Swaziland
Sweden
Switzerland
Syria
Taiwan
Tajikistan
Tanzania
Thailand
Timor-Leste
Togo
Tonga
Trinidad and Tobago
Tunisia
Turkey
Turkmenistan
Tuvalu
Uganda
Ukraine
United Arab Emirates(UAE)
United Kingdom(UK)
United States of America(USA)
Uruguay
Uzbekistan
Vanuatu
Vatican City(Holy See)
Venezuela
Vietnam
Yemen
Zambia
Zimbabwe


static final String[] COUNTRIES = new String[] {
          "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra",
          "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina",
          "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
          "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
          "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia",
          "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory",
          "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
          "Cote d'Ivoire", "Cambodia", "Cameroon", "Canada", "Cape Verde",
          "Cayman Islands", "Central African Republic", "Chad", "Chile", "China",
          "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo",
          "Cook Islands", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic",
          "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica", "Dominican Republic",
          "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea",
          "Estonia", "Ethiopia", "Faeroe Islands", "Falkland Islands", "Fiji", "Finland",
          "Former Yugoslav Republic of Macedonia", "France", "French Guiana", "French Polynesia",
          "French Southern Territories", "Gabon", "Georgia", "Germany", "Ghana", "Gibraltar",
          "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau",
          "Guyana", "Haiti", "Heard Island and McDonald Islands", "Honduras", "Hong Kong", "Hungary",
          "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica",
          "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Laos",
          "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg",
          "Macau", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands",
          "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova",
          "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia",
          "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand",
          "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "North Korea", "Northern Marianas",
          "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
          "Philippines", "Pitcairn Islands", "Poland", "Portugal", "Puerto Rico", "Qatar",
          "Reunion", "Romania", "Russia", "Rwanda", "Sqo Tome and Principe", "Saint Helena",
          "Saint Kitts and Nevis", "Saint Lucia", "Saint Pierre and Miquelon",
          "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Saudi Arabia", "Senegal",
          "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
          "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "South Korea",
          "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden",
          "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "The Bahamas",
          "The Gambia", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey",
          "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Virgin Islands", "Uganda",
          "Ukraine", "United Arab Emirates", "United Kingdom",
          "United States", "United States Minor Outlying Islands", "Uruguay", "Uzbekistan",
          "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Wallis and Futuna", "Western Sahara",
          "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"
        };
        

*/
