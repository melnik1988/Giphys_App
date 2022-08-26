using System.Collections.Generic;

namespace Giphy_App.Moduls.Responces
{
    public class Giphys
    {
        public List<Data>? data;
        public Pagination? pagination;
        public Meta? meta;
    }

    public class Data
    {
        public string? type;
        public string? url;
        public string? title;
        public string? rating;
        public string? import_datetime;
        public Image? images;
    }

    public class Image
    {
        public Original? original;
    }

    public class Original
    {
        public string? height;
        public string? width;
        public string? url;
        public string? hash;
    }

    public class Pagination
    {
        public int? total_count;
        public int? count;
        public int? offset;
        
    }

    public class Meta
    {
        public int? status;
        public string? msg;
    }
}
