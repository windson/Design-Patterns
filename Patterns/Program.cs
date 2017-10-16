using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            NAVULE n = new NAVULE("NAVULE", 100000.53);
            n.Attach(new Investor("Accel"));
            n.Attach(new Investor("Tiger"));
            n.Price = 100010;
            n.Price = 100100;
            n.Price = 100340;
            Console.ReadKey();
        }
        //Subject(Stock)
        //knows its observers.Any number of Observer objects may observe a subject
        //provides an interface for attaching and detaching Observer objects.
        abstract class Stock
        {
            private double _price;
            private string _symbol;
            List<IInvestor> Investors = new List<IInvestor>();
            public Stock(string symbol, double price)
            {
                Symbol = symbol;
                Price = price;
            }
            public void Attach(IInvestor investor)
            {
                Investors.Add(investor);
            }
            public void Detach(IInvestor investor)
            {
                Investors.Remove(investor);
            }
            public void Notify()
            {
                Investors.ForEach(x => x.Update(this));
            }

            public string Symbol
            {
                get => _symbol;
                set => _symbol = value;
            }
            public double Price
            {
                get => _price;
                set { _price = value; Notify(); }
            }
        }

        //ConcreteSubject(IBM)
        //stores state of interest to ConcreteObserver
        //sends a notification to its observers when its state changes
        class NAVULE : Stock
        {
            public NAVULE(string symbol, double price) : base(symbol, price)
            {
            }
        }

        //Observer(IInvestor)
        //defines an updating interface for objects that should be notified of changes in a subject.
        interface IInvestor
        {
            void Update(Stock stock);
        }

        //ConcreteObserver(Investor)
        //maintains a reference to a ConcreteSubject object
        //stores state that should stay consistent with the subject's
        //implements the Observer updating interface to keep its state consistent with the subject's
        class Investor : IInvestor
        {
            public Investor(string name)
            {
                _name = name;
            }
            public void Update(Stock stock)
            {
                _stock = stock;
                Console.WriteLine($"Dear {_name} {_stock.Symbol} got updated to {_stock.Price}");
            }

            private Stock _stock;
            private string _name;

            public Stock Stock
            {
                get => _stock;
                set => _stock = value;
            }

        }
    }

}
