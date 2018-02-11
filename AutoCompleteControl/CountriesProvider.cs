using System;
using Android.Content;
using Android.Database;
using Android.Net;

namespace AutoCompleteControl
{
    [ContentProvider(new string[] { CountriesProvider.AUTHORITY })]
    public class CountriesProvider : ContentProvider
    {
        CountriesDatabase _cdb;
        static string BASE_PATH = "coutries";

        const int GET_ALL = 0;
        const int GET_ONE = 1;
        static UriMatcher uriMatcher = BuildUriMatcher();

        static UriMatcher BuildUriMatcher()
        {
            var matcher = new UriMatcher(UriMatcher.NoMatch);
            matcher.AddURI(AUTHORITY, BASE_PATH, GET_ALL);
            matcher.AddURI(AUTHORITY, BASE_PATH + "/#", GET_ONE);
            return matcher;
        }

        public const string AUTHORITY = "com.letsolutions.AutoCompleteControl.CountriesProvider";
        public static readonly Android.Net.Uri CONTENT_URI = Android.Net.Uri.Parse("content://" + AUTHORITY + "/" + BASE_PATH);

        // MIME types used for getting a list, or a single vegetable
        public const string COUNTRIES_MIME_TYPE = ContentResolver.CursorDirBaseType + "/vnd.com.letsolutions.AutoCompleteControl.Countries";
        public const string COUNTRY_MIME_TYPE = ContentResolver.CursorDirBaseType + "/vnd.com.letsolutions.AutoCompleteControl.Countries";

        // Calumn Names
        public static new class InterfaceConsts
        {
            public const string Id = CountriesDatabase.FieldId;
            public const string Name = CountriesDatabase.FieldName;
        }

        public override string GetType(Android.Net.Uri uri)
        {
            switch (uriMatcher.Match(uri))
            {
                case GET_ALL:
                    return COUNTRIES_MIME_TYPE;
                case GET_ONE:
                    return COUNTRY_MIME_TYPE;
                default:
                    throw new Java.Lang.IllegalArgumentException("Unknown Uri:" + uri);
            }
        }

        public override bool OnCreate()
        {
            _cdb = new CountriesDatabase(Context);
            return true;
        }

        public override ICursor Query(Android.Net.Uri uri, string[] projection, string selection, string[] selectionArgs, string sortOrder)
        {
            switch (uriMatcher.Match(uri))
            {
                case GET_ALL:
                    return GetCountries();
                case GET_ONE:
                    var id = uri.LastPathSegment;
                    return GetCountryById(id);
                default:
                    throw new Java.Lang.IllegalArgumentException("Unknown Uri:" + uri);
            }
        }

        public override int Update(Android.Net.Uri uri, ContentValues values, string selection, string[] selectionArgs)
        {
            throw new Java.Lang.UnsupportedOperationException();
        }

        public override Android.Net.Uri Insert(Android.Net.Uri uri, ContentValues values)
        {
            throw new Java.Lang.UnsupportedOperationException();
        }

        public override int Delete(Android.Net.Uri uri, string selection, string[] selectionArgs)
        {
            throw new Java.Lang.UnsupportedOperationException();
        }

        private ICursor GetCountryById(string id)
        {
            return _cdb.GetCountry(id);
        }
        private ICursor GetCountries()
        {
            return _cdb.GetCountries();
        }
        private ICursor GetCountriesBySerach(string searchText)
        {
            return _cdb.GetCountries(searchText);
        }
        private ICursor GetCountriesBySearch(string searchText, uint limit)
        {
            return _cdb.GetCountries(searchText, limit);
        }
    }
}
