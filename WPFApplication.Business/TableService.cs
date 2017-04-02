using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApplication.DataAccess;
using WPFApplication.Model;

namespace WPFApplication.Business
{
    public class TableService: BaseService
    {
        private readonly TableDataClass tableDataClass;


        public TableService()
        {
            this.tableDataClass = new TableDataClass(GetConnectionString());
        }

        //retourneert een tafel met een bepaald id
        public Table GetById(int id)
        {
            //Validation
            if (id == 0)
            {
                return null;
            }

            return tableDataClass.GetById(id);
        }

        //retourneert alle tafels
        public ICollection<Table> GetAll()
        {
            return tableDataClass.GetAll();
        }

        //voegt een tafel toe
        public void InsertTable(Table table)
        {
                tableDataClass.InsertTable(table);
           
            
           
        }

        //past een bestaande tafel aan
        public void UpdateTable(Table table)
        {
            tableDataClass.UpdateTable(table);
        }

        //verwijderd een bepaalde tafel
        public void DeleteTable(Table table)
        {
            tableDataClass.DeleteTable(table);
        }

        //kijkt of alle velden ingevuld zijn van een tafel object
        public bool CheckAllFieds(Table tbl)
        {

            bool isComplete = true;

            if (string.IsNullOrEmpty(tbl.Name))
            {
                isComplete = false;


            } else if (tbl.Height == 0 && tbl.Width == 0) { }
          
            {
                isComplete = false;

               
            }
       


            return isComplete;

        }











    }
}
