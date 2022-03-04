namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserListItem
    {
        public int CanvasserId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAbsent { get; set; }  
    }
}
