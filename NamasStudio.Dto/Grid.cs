namespace NamasStudio.Dto {
    public class Grid<T> {
        public int Count { get; set; }

        public List<T> Data { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public double TotalPages { get {
                return Math.Ceiling((double)Count / this.PageSize);
            } }

    }
}