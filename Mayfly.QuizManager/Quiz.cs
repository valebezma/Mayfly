
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.IO.Compression;
using System.Text;
using Mayfly.Extensions;


namespace QuizManager
{


    public partial class Quiz
    {
        #region IO

        public string SuggestedName
        {
            get
            {
                if (Game.Count == 1)
                {
                    GameRow gameRow = Game[0];

                    string result = string.Empty;

                    if (!gameRow.IsTitleNull())
                    {
                        result += gameRow.Title + " ";
                    }

                    return result.Trim() + UserSettings.Interface.Extension;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool Read(string fileName)
        {
            try
            {
                base.ReadXml(fileName);
                return Game.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public void WriteToFile(string fileName)
        {
            File.WriteAllText(fileName, GetXml());
        }

        public static Quiz FromClipboard()
        {
            Quiz data = new Quiz();
            data.ReadXml(new StringReader(Clipboard.GetText()));
            return data;
        }

        #endregion



        public Quiz.GameRow Solitary
        {
            get
            {
                if (Game.Rows.Count == 0)
                {
                    GameRow result = Game.NewGameRow();
                    Game.AddGameRow(result);
                    return result;
                }
                else
                {
                    return Game[0];
                }
            }
        }

        partial class GameDataTable
        {
        }
    }
}