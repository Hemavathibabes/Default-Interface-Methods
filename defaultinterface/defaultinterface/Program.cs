using System;
using System.IO;
using System.Threading.Tasks;

namespace defaultinterface
{
    //Normal book status (its shows the book is returned to the library or not)
    interface Ibook
    {
        void isavailable();
        void isnotavailable();
        bool isstatus();
    }
    class student:Ibook
    {
        public bool status;
        public bool isstatus()
        {
            return status;
        }
        public void isavailable()
        {
            status = true;
        }
        public void isnotavailable()

        {
            status = false ;
        }
        public void finalstatus()
        {
            if (status)
            {
                Console.WriteLine("The book is available");
            }
            else
            {
                Console.WriteLine("The book is not available");
            }
        }
    }
    //The book is kept by the student with the particular duration
    interface Iduedatetobook :Ibook
    {
        public async Task untilltime(int duration)
        {
            Console.WriteLine("Using a default implemented interface methods ");
            isnotavailable();
            await Task.Delay(duration);
            isavailable();

        }
    }
    //student status in library with duetime
   /* class student : Ibook,Iduedatetobook
    {
        public bool status;
        public bool isstatus()
        {
            return status;
        }
        public void isavailable()
        {
            status = true;
        }
        public void isnotavailable()

        {
            status = false;
        }
       
    }*/
    class studenttimer : Ibook , Iduedatetobook
    {
        private enum studentstatus
        { 
            notavailable,
            available,
            duetime
        }
        private studentstatus s;
        public void isavailable()
        {
            s = studentstatus.notavailable;
        }
        public void isnotavailable()
        {
            s = studentstatus.available;
        }
        public bool isstatus()
        {
            return s != studentstatus.notavailable;
        }
        public async Task untilltime(int duration)
        {
            s = studentstatus.duetime;
            await Task.Delay(duration);
            s = studentstatus.available;
            Console.WriteLine("the book is available now!!");
        }
        public override String ToString()
        {
            
            return $"the book is returned {s}";
        }
        
    }

    //The book is borrowed and kept by a student within the period of time
    //The book in this library available within the time period
    interface Itimeperiod : Ibook
    {
        public async Task particulartimer(int duration, int repeat)
        {

            Console.WriteLine("the interface shows the book is available for particular period of time");
            for (int count = 0; count < repeat; count++)
            {
                isnotavailable();
                await Task.Delay(duration);
                isavailable();
                await Task.Delay(duration);

            }
        }
    }
    //now the student can inherit Iduedatetobook,Itimeperiod
   /* class student : Ibook,Iduedatetobook,Itimeperiod
    {
        public bool status;
        public bool isstatus()
        {
            return status;
        }
        public void isavailable()
        {
            status = true;
        }
        public void isnotavailable()

        {
            status = false;
        }
        public void finalstatus()
        {
            if (status)
            {
                Console.WriteLine("the book is available");
            }
            else
            {
                Console.WriteLine("the book is not available");
            }
        }
    }*/
    //the new class which inherit the directly use one feature
    class UGstudent:Ibook,Iduedatetobook,Itimeperiod
        {
        public bool status;
        public bool isstatus()
        {
            return status;
        }
        public void isavailable()
        {
            status = true;
        }
        public void isnotavailable()

        {
            status = false;
        }
        public async Task particulartimer(int duration,int repeat)
            {
            Console.WriteLine("the book is not available");
            await Task.Delay(duration * repeat);
            Console.WriteLine("the book is available now!");

        }
        public void finalstatus()
        {
            if (status)
            {
                Console.WriteLine("the book is available");
            }
            else
            {
                Console.WriteLine("the book is not available");
            }
        }
    }
    //The class directly use both features
    class PGstudent : Ibook, Iduedatetobook, Itimeperiod
    {
        public bool status;
        public bool isstatus()
        {
            return status;
        }
        public void isavailable()
        {
            status = true;
        }
        public void isnotavailable()

        {
            status = false;
        }
        public async Task particulartimer(int duration, int repeat)
        {
            Console.WriteLine("The book is not available");
            await Task.Delay(duration * repeat);
            Console.WriteLine("The book is available now!");

        }
        public async Task untilltime(int duration)
        {
            Console.WriteLine("The book is not available");
            await Task.Delay(duration);
            Console.WriteLine("The book is available");
        }
        public void finalstatus()
        {
            if (status)
            {
                Console.WriteLine("The book is available");
            }
            else
            {
                Console.WriteLine("The book is not available");
            }
        }
    }
    class Program
    {
        private static async Task bookavaliabilites(Ibook book)
        {
          if(book is  Iduedatetobook Duetime)
            {
                Console.WriteLine("Duetimer funciton");
                await Duetime.untilltime(60000);
                Console.WriteLine("Function end");

            }
            else
            {
                Console.WriteLine("The function does not work");
            }
          if(book is Itimeperiod periodtime)
            {
                Console.WriteLine("Period of time function start");
                await periodtime.particulartimer(3000, 30);
                Console.WriteLine("Function end");

            }
            else
            {
                Console.WriteLine("The function does not support");
            }
        }
       
        static async Task Main(string[] args)
        {
            Console.WriteLine("The normal status of book in a library:");
            student obj = new student();
            obj.isnotavailable();
            obj.finalstatus();
            obj.isavailable();
            obj.finalstatus();
            
            Console.WriteLine("Return the book within the due time:");
            var studenttimer= new studenttimer();
            await bookavaliabilites(studenttimer);
            Console.WriteLine();

            Console.WriteLine("The book is availble on the library for the particular time");
            var ug = new UGstudent();
            await bookavaliabilites(ug);
            Console.WriteLine();

            Console.WriteLine("PG students can have a both features:");
            var pg = new PGstudent();
            await bookavaliabilites(pg);
            Console.WriteLine();
        }
    }
}
