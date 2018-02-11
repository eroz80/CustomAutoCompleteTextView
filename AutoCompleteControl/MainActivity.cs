using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Net;
using Android.Database;
using System.Collections.Generic;

namespace AutoCompleteControl
{
    [Activity(Label = "AutoCompleteControl", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        SimpleCursorAdapter adapter;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var cdb = new CountriesDatabase(this);
            cdb.InitDatabase();

            // Para grandes cantidades de datos, es necesario utilizar CursorAdapter
            var autocompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteInput);

            string[] fromColumns = new string[] { CountriesProvider.InterfaceConsts.Name };
            int[] toControls = new int[] { Android.Resource.Id.Text1 };
            adapter = new SimpleCursorAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, null, fromColumns, toControls);
            adapter.CursorToStringConverter = new CustomCursorToStringConverter();
            adapter.FilterQueryProvider = new CustomFilterQueryProvider(this, 5);
            autocompleteTextView.Adapter = adapter;

        }
    }
}

