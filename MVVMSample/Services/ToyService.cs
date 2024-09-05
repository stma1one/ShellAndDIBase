using MVVMSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSample.Services;


interface IToys
{
    
    //בשלב זה רק לדעת מה הפעולות שניתן לבצע באמצעות השרות
    List<Toy>? GetToys();//החזרת אוסף צעצועים
    List<ToyTypes>? GetToyTypes();//החזרת סוגי צעצועים
    public List<Toy> GetToyByType(ToyTypes type);//החזרת אוסף צעצועים לפי סוג
    public List<Toy>? GetToysByPriceCondition(double price, bool abovePrice);//החזרת צעצועים מעל או מתחת מחיר
    public List<Toy>? GetToysByPriceCondition(Predicate<double> condition);//החזרת צעצועים עפ"י תנאי
    public bool AddToy(Toy toy);//הוספת צעצוע
    public bool DeleteToy(Toy toy);//מחיקת צעצוע
}






//בעתיד הפעולות יפנו לשרת DB
    class ToyService:IToys
    {
        int id;
        List<Toy>? toys;
        List<ToyTypes>? toyTypes;
        public ToyService()
        {
            InitToyTypes();
            InitToys();
            id = 0;
        }

        private void InitToyTypes()
        {
           toyTypes= new List<ToyTypes>()
        {
            new ToyTypes()
            {
                Id = 1, Name = "פאזל"
            },

            new ToyTypes()
            {
            Id = 2, Name = "משחקי חשיבה"
            },
            new ToyTypes()
            {
            Id = 3, Name = "בובה"
            }
        };
        }

        private void InitToys()
        {
            toys = new List<Toy>()
            {
                new Toy()
                {
                    Id=++id,
                    Image="chuky.jpg",
                    IsSecondHand=false,
                    Name="צאקי",
                    Price=200,
                    Type=toyTypes[2]
                },
                new Toy()
                {
                    Id=++id,
                    Image="puppet.jpeg",
                    IsSecondHand=false,
                    Name="רובי",
                    Price=250,
                    Type=toyTypes[2]
                },
                new Toy()
                {
                    Id=++id,
                    Image="puzzle.jpeg",
                    IsSecondHand=false,
                    Name="גן חיות",
                    Price=250,
                    Type=toyTypes[0]
                },
                new Toy()
                {
                    Id=++id,
                    Image="thinkgame.jpeg",
                    IsSecondHand=true,
                    Name="מבוכים ודרקונים",
                    Price=250,
                    Type=toyTypes[1]
                }

            };
        }

        public List<Toy>? GetToys()
        {
            return toys.ToList();
        }

        public List<Toy> GetToyByType(ToyTypes type)
        {
            #region By LINQ
            //   return  toys.Where(t=>t.Type.Id==type.Id).ToList();
            #endregion
            List<Toy> result=new();
            foreach (var t in toys)
            {
                if(t.Type.Id==type.Id)
                    result.Add(t);
            }
            return result;
        }


        public List<Toy>? GetToysByPriceCondition(double price, bool abovePrice)
        {
            List<Toy> result = new();
            foreach (var t in toys)
            {
                if (abovePrice)
                {
                    if (t.Price > price)
                        result.Add(t);
                }
                else
                    if (t.Price <= price)
                    result.Add(t);
            }
            return result;
        }

        #region Filter By delegate
        public List<Toy>? GetToysByPriceCondition(Predicate<double> condition)
        {
            return toys?.Where(x => condition(x.Price)).ToList();
        }
        #endregion

       
    
    
    public bool AddToy(Toy toy)
        {
            if (toys != null&&!(toys.Any(t=>t.Name==toy.Name&&t.IsSecondHand==toy.IsSecondHand)))
            {
                toys.Add(toy);
                return true;
            }
            return false;

        }

        public bool DeleteToy(Toy toy)
        {
            foreach (var t in toys)
            {
                if(t.Id==toy.Id)
                    return toys.Remove(t);  
            }
            return false;
            #region using LINQ
            // return toys.Remove(toys.Find((x)=>x.Id==toy.Id));
            #endregion
        }

    public List<ToyTypes>? GetToyTypes()
    {
       return toyTypes? .ToList();
    }
}

