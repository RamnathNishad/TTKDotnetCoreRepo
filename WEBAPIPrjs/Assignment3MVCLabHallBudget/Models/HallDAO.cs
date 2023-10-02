namespace Assignment3MVCLabHallBudget.Models
{
    public class HallDAO
    {

        List<Hall> halls;
        public HallDAO()
        {
                halls = new List<Hall>
                {
                    new Hall{ id=1,hallName="Hall Paradise",ownerName="Steve Jobs",costPerDay=40000,mobile="987654321",address="Washington"},
                    new Hall{ id=2,hallName="Rudolfinum",ownerName="Othello",costPerDay=35000,mobile="987654554",address="Denmark"},
                    new Hall{ id=3,hallName="Casa da Musica",ownerName="Jason Smith",costPerDay=25000,mobile="987657654",address="Portugal"},
                    new Hall{ id=4,hallName="Palico de Bellas Artes",ownerName="Bratt",costPerDay=32000,mobile="987654755",address="New York"},
                    new Hall{ id=5,hallName="Esplanade",ownerName="Bobby",costPerDay=30000,mobile="987654877",address="Singapore"}
                };
        }

        public List<Hall> GetHall(int price)
        {
            return halls.Where(o=>o.costPerDay<=price).ToList();
        }
    }
}
