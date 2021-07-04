using System;
using System.Data;

namespace Mayfly.Wild
{
    public interface IBioable
    {
        DataRow[] GetBioRows(string species);

        double GetIndividualValue(DataRow individualRow, string field);

        void InitializeBio();

        void RefreshBios();

        IBio GetBio(string species, string x, string y);

        string[] GetAuthors();

        DateTime[] GetDates();

        string[] GetPlaces();
    }
}
