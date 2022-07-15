/*------------------------------------------------------------------------------------------------------------------------------------------------
Description     : Create a library application that uses only data structures to store user, book, and author details. 
Name            : Venkatesan Manivasagam
Date Created    : 15/07/2022
-------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Library
    {
        List<string> lstUserName = new List<string>();
        Dictionary<string, string> dictAuthor = new Dictionary<string, string> { };
        Dictionary<string, dynamic> dictBookCopy = new Dictionary<string, dynamic> { };
        Dictionary<string, dynamic> dictLoanBook = new Dictionary<string, dynamic> { };
        Dictionary<string, dynamic> dictReturnedBooks = new Dictionary<string, dynamic> { };

        // Add user to the system. 
        public void addUser(string strName)
        {
            if (!string.IsNullOrWhiteSpace(strName))
            {
                lstUserName.Add(strName);
            }
        }

        // Return list of user in the system. 
        public string returnUser()
        {
            if (lstUserName.Count != 0)
            {
                return "Available Users: " + (string.Join(",", lstUserName));
            }
            else
            {
                return "Currently no users in the System";
            }
        }

        // Add Author and their Genre specification into the system. 
        public void addAuthor(string strAuhorName, string strAuthorGenre)
        {
            if (!string.IsNullOrWhiteSpace(strAuhorName) && !string.IsNullOrWhiteSpace(strAuthorGenre))
            {
                dictAuthor.Add(strAuhorName, strAuthorGenre);
            }
        }

        // Return list of available Authors in the system. 
        public string returnAuthors()
        {
            string authorList = "List of Authors \n";
            if (dictAuthor.Count != 0)
            {
                foreach (KeyValuePair<string, string> kvp in dictAuthor)
                {                 
                    authorList += "\t" + kvp.Key + " of Genre " + kvp.Value + " .\n";
                }
                return authorList;
            }
            else { return "Currently No authors in the system."; }
        }

        // Add book copy to the system. 
        public void addBookCopy(string strauthorName, string strBookName)
        {
            if (!string.IsNullOrWhiteSpace(strauthorName) && !string.IsNullOrWhiteSpace(strBookName))
            {
                if (!dictBookCopy.ContainsKey(strauthorName))
                {
                    dictBookCopy.Add(strauthorName, (strBookName, 1, 0));
                }
                else
                {
                    dictBookCopy[strauthorName].Item2 = dictBookCopy[strauthorName].Item2 + 1;
                }
            }
        }

        // Return list of available Book copies (that are not in Loan). 
        public string returnAvailableBookCopies()
        {
            string strBookCopy = "List of Books Available \n"; ;
            if (dictBookCopy.Count != 0)
            {
                foreach (KeyValuePair<string, dynamic> kvp in dictBookCopy)
                {
                    if (kvp.Value.Item3 < kvp.Value.Item2)
                    {
                        strBookCopy += "\t" + "Author Name: " + kvp.Key + " ;Book Name: " + kvp.Value.Item1 + " ;Count: " + kvp.Value.Item2 + " ;isLoan: " + kvp.Value.Item3 + " .\n"; //Impr : String Builder.
                    }
                }
                return strBookCopy;
            }
            else { return "No Books available in the System."; }
        }


        // Loan specified book to the user. 
        public int loanBook(string strName, string strBookName, int year, int month, int day)
        {
            if (!string.IsNullOrWhiteSpace(strName) && !string.IsNullOrWhiteSpace(strBookName) && year != 0 && month != 0 && day != 0)
            {
                if (lstUserName.Contains(strName))
                {
                    foreach (KeyValuePair<string, dynamic> kvp in dictBookCopy)
                    {
                        if (kvp.Value.Item1 == strBookName)
                        {
                            if (kvp.Value.Item3 < kvp.Value.Item2)
                            {
                                if (!dictLoanBook.ContainsKey(strName))
                                {
                                    dictLoanBook.Add(strName, (strBookName, year, month, day));
                                    kvp.Value.Item3 += 1;
                                    return 1;
                                }
                            }
                        }
                    }
                }

            }
            return 0;
        }

        // Return list of Loaned books within the system.
        public string returnLoanBooks()
        {
            string strBooksOnLoan = "List of Books in Loan \n";
            if (dictBookCopy.Count != 0 && dictLoanBook.Count != 0)
            {
                foreach (KeyValuePair<string, dynamic> kvp in dictBookCopy)
                {
                    foreach (KeyValuePair<string, dynamic> kvp2 in dictLoanBook)
                    {

                        if (kvp.Value.Item1 == kvp2.Value.Item1)
                        {
                            if (kvp.Value.Item3 > 0)
                            {
                                strBooksOnLoan += "\t" + "Book: " + kvp2.Value.Item1 + " ;Author Name: " + kvp.Key + " .\n";
                            }
                        }
                    }
                }
                return strBooksOnLoan;
            }
            else
            {
                return "No books loaned in the System.";

            }
        }

        // Return loaned books back to the system.
        public void returnLoanedBooks(string strName, string strBookName, int year, int month, int day)
        {
            if (!string.IsNullOrWhiteSpace(strName) && !string.IsNullOrWhiteSpace(strBookName) && year != 0 && month != 0 && day != 0)
            {
                foreach (KeyValuePair<string, dynamic> kvp in dictBookCopy)
                {
                    if (kvp.Value.Item1 == strBookName && kvp.Value.Item3 > 0)
                    {
                        if (dictLoanBook.ContainsKey(strName))
                        {
                            kvp.Value.Item3 -= 1;
                            dictReturnedBooks.Add(strName, (strBookName, year, month, day));
                        }
                    }
                }
            }
        }

        // Delete books from the system.
        public void deleteBooks(string strBookName)
        {
            if (!string.IsNullOrWhiteSpace(strBookName))
            {
                if (dictBookCopy.Count != 0)
                {
                    foreach (KeyValuePair<string, dynamic> kvp in dictBookCopy)
                    {
                        foreach (KeyValuePair<string, dynamic> kvp2 in dictLoanBook)
                        {
                            if (kvp2.Value.Item1 == kvp.Value.Item1 && kvp.Value.Item3 == 0)
                            {
                                dictBookCopy.Remove(kvp.Key);
                            }
                        }
                    }
                }
            }

        }

        // Delete users from the system. 
        public void deleteUsers(string strName)
        {
            if (!string.IsNullOrWhiteSpace(strName))
            {
                if (!dictLoanBook.ContainsKey(strName))
                {
                    lstUserName.Remove(strName);

                }
            }
        }

        // Return books loaned by user within the specific time. 
        public string returnBooksLoanedByUser(string strName, int startYear, int startMonth, int startDate, int endYear, int endMonth, int endDate)
        {
            if (!string.IsNullOrWhiteSpace(strName) && startDate != 0 && startMonth != 0 && startYear != 0 && endDate != 0 && endMonth != 0 && endDate != 0)
            {
                DateTime dtCheckStartDate = new DateTime(startYear, startMonth, startDate);
                DateTime dtCheckEndDate = new DateTime(endYear, endMonth, endDate);
                List<string> lstLoanList = new List<string>();

                if (dictLoanBook.ContainsKey(strName) || dictReturnedBooks.ContainsKey(strName))
                {
                    foreach (KeyValuePair<string, dynamic> kvp in dictLoanBook)
                    {
                        foreach (KeyValuePair<string, dynamic> kvp2 in dictReturnedBooks)
                        {

                            DateTime dtLoanStartDate = new DateTime(kvp.Value.Item2, kvp.Value.Item3, kvp.Value.Item4);
                            DateTime dtLoanEndDate = new DateTime(kvp2.Value.Item2, kvp2.Value.Item3, kvp.Value.Item4);
                            //string strLoanEndDate = Convert.ToString(kvp2.Value.Item2) + Convert.ToString(kvp2.Value.Item3) + Convert.ToString(kvp2.Value.Item4);
                            if (dtCheckStartDate <= dtLoanStartDate && dtCheckEndDate >= dtLoanEndDate && kvp.Key == strName && kvp2.Key == strName)
                            {
                                lstLoanList.Add(kvp.Value.Item1);
                            }
                        }
                    }
                }
                return string.Join(",", lstLoanList);

            }
            return "Requested user - \"" + strName + "\" is not available in the system.";
        }
    }
}
