using System.Collections.Generic;

namespace Mayfly.Library
{
    public partial class Library
    {
        partial class BookDataTable
        {
            public BookRow GetSame(BookRow bookRow)
            {
                BookRow result = null;
                
                if (!bookRow.IsISBNNull()) result = FindByISBN(bookRow.ISBN);

                if (result != null) return result;
                
                result = FindByTitle(bookRow.Title, bookRow.Volume, bookRow.Issue);

                if (result != null) return result;

                return result;
            }

            public BookRow FindByISBN(string isbn)
            {
                foreach (BookRow dataRow in Rows)
                {
                    if (dataRow.IsNull(columnISBN))
                    {
                        continue;
                    }

                    if (dataRow.ISBN == isbn)
                    {
                        return dataRow;
                    }
                }
                return null;
            }

            public BookRow FindByTitle(string title, string volume, string issue)
            {
                foreach (BookRow dataRow in Rows)
                {
                    if (dataRow.IsNull(columnTitle))
                    {
                        continue;
                    }

                    if (dataRow.IsNull(columnVolume))
                    {
                        continue;
                    }

                    if (dataRow.IsNull(columnIssue))
                    {
                        continue;
                    }

                    if (dataRow.Title == title && dataRow.Volume == volume && dataRow.Issue == issue)
                    {
                        return dataRow;
                    }
                }
                return null;
            }
        }

        partial class BookRow
        {
            public AuthorRow GetExecutive()
            {
                return this.IsExeIDNull() ? null : (AuthorRow)this.GetParentRow("Executive");
            }

            public AuthorRow GetApprovedBy()
            {
                return this.IsAppIDNull() ? null : (AuthorRow)this.GetParentRow("Approved");
            }

            public string[] GetKeywords()
            {
                List<string> result = new List<string>();

                foreach (KeywordsRow row in this.GetKeywordsRows())
                {
                    result.Add(row.TerminRow.Word);
                }

                return result.ToArray();
            }

            public string[] GetAuthors()
            {
                List<string> result = new List<string>();

                foreach (Library.BylineRow row in this.GetBylineRows())
                {
                    result.Add(row.AuthorRow.Name);
                }

                return result.ToArray();
            }
        }
        
        public int ItemsCount
        {
            get
            {
                int result = 0;

                foreach (BookRow bookRow in Book)
                {
                    result += bookRow.Quantity;
                }

                return result;
            }
        }
        
        public int BooksCount
        {
            get
            {
                int result = 0;

                foreach (BookRow bookRow in Book)
                {
                    if (bookRow.Type == 1)
                    {
                        result += bookRow.Quantity;
                    }
                }

                return result;
            }
        }
        
        public int ResearchesCount
        {
            get
            {
                int result = 0;

                foreach (BookRow bookRow in Book)
                {
                    if (bookRow.Type == 2)
                    {
                        result += bookRow.Quantity;
                    }
                }

                return result;
            }
        }

        partial class PublisherDataTable
        {
            public PublisherRow FindByNameAndWhere(string name, string where)
            {
                if (string.IsNullOrWhiteSpace(name)) return null;

                if (string.IsNullOrWhiteSpace(where)) return null;

                foreach (PublisherRow dataRow in Rows)
                {
                    if (dataRow.IsNull(columnName))
                    {
                        continue;
                    }

                    if (dataRow.IsNull(columnWhere))
                    {
                        continue;
                    }

                    if (dataRow.Name == name &&
                        dataRow.Where == where)
                    {
                        return dataRow;
                    }
                }

                return null;
            }
        }

        partial class AuthorDataTable
        {
            public AuthorRow FindByName(string name)
            {
                foreach (AuthorRow dataRow in Rows)
                {
                    if (dataRow.IsNull(columnName))
                    {
                        continue;
                    }

                    if (dataRow.Name == name)
                    {
                        return dataRow;
                    }
                }
                return null;
            }
        }

        partial class RackDataTable
        {
            public RackRow FindByName(string name)
            {
                foreach (RackRow dataRow in Rows)
                {
                    if (dataRow.IsNull(columnName))
                    {
                        continue;
                    }

                    if (dataRow.Name == name)
                    {
                        return dataRow;
                    }
                }
                return null;
            }
        }
    }
}
