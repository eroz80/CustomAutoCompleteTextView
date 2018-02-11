using System;
using Android.Content;
using Android.Database;
using Android.Views;
using Android.Widget;
using Java.Lang;
using static Android.Widget.SimpleCursorAdapter;

namespace AutoCompleteControl
{
    public class CustomCursorToStringConverter : Java.Lang.Object, ICursorToStringConverter
    {
        public ICharSequence ConvertToStringFormatted(ICursor cursor)
        {
            return new Java.Lang.String(cursor.GetString(cursor.GetColumnIndexOrThrow(CountriesProvider.InterfaceConsts.Name)));
        }
    }

    public class CustomFilterQueryProvider : Java.Lang.Object, IFilterQueryProvider
    {
        Context _context;
        uint _limit;
        public CustomFilterQueryProvider(Context context, uint limit = 5)
        {
            _context = context;
            _limit = limit;
        }
        public ICursor RunQuery(ICharSequence constraint)
        {
            if (constraint != null)
            {
                var cdb = new CountriesDatabase(_context);
                return cdb.GetCountries(constraint.ToString(), _limit);
            }
            return null;
        }
    }
}
