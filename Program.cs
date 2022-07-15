using System;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Library objLibrary = new Library();
            objLibrary.addUser("Venkatesan");
            objLibrary.addUser("Sathish");
            objLibrary.addUser("Madhu");
            Console.WriteLine(objLibrary.returnUser());
            objLibrary.addAuthor("Jamce Joyce", "Fiction");
            objLibrary.addAuthor("Jordan Petereson", "Psychology");
            objLibrary.addAuthor("Sir Arthur Conan Doyle ", "Detective");
            Console.WriteLine(objLibrary.returnAuthors());
            objLibrary.addBookCopy("Jamce Joyce", "ulysses");
            objLibrary.addBookCopy("Jordan Petereson", "12 Rules For Life");
            objLibrary.addBookCopy("Sir Arthur Conan Doyle", "Sherlock Holmes");
            objLibrary.addBookCopy("Sir Arthur Conan Doyle", "Sherlock Holmes");
            Console.WriteLine(objLibrary.returnAvailableBookCopies());
            objLibrary.loanBook("Venkatesan", "ulysses", 2019, 1, 3); // Case sensitive when comparing.     
            objLibrary.loanBook("Sathish", "Sherlock Holmes", 2019, 1, 3); // Case sensitive when comparing.     
            objLibrary.loanBook("Venkat", "Sherlock Holmes", 2019, 1, 3);
            Console.WriteLine(objLibrary.returnAvailableBookCopies());
            Console.WriteLine(objLibrary.returnLoanBooks());
            objLibrary.returnLoanedBooks("Venkatesan", "ulysses", 2019, 2, 4);
            Console.WriteLine(objLibrary.returnLoanBooks());
            Console.WriteLine(objLibrary.returnAvailableBookCopies());
            objLibrary.deleteBooks("ulysses");
            Console.WriteLine(objLibrary.returnAvailableBookCopies());
            objLibrary.deleteUsers("Madhu");
            Console.WriteLine(objLibrary.returnBooksLoanedByUser("Venkatesan",2019,1,2,2019,2,5));
        }
    }
}
