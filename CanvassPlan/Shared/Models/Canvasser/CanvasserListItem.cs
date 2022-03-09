namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserListItem
    {
        public int CanvasserId { get; set; }
        public string Name { get; set; }
        public bool Inactive { get; set; }
        public bool IsAbsent { get; set; }  
        public bool IsDriver { get; set; }
        public bool IsLeader { get; set; }
        public bool IsTraining { get; set; }
    }
}
