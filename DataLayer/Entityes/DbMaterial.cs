namespace DataLayer.Entityes {
	public class DbMaterial : Page {
		public int DirectoryId { get; set; }
		
		public DbDirectory Directory { get; set; }
	}
}