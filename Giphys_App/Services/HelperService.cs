using Giphy_App.Moduls.Responces;
using Giphys_App.Interface;
using Giphys_App.Moduls.Classes;

namespace Giphys_App.Services
{
    public class HelperService : IHelper
    {
        public HelperService()
        {
        }

        public GiphyURL takeOutURL(Giphys giphys,string str)
        {
            GiphyURL urlList = new GiphyURL();
            urlList.query = str;
            try
            {
                if (giphys != null)
                {
                    if (giphys.data != null)
                        foreach (Data d in giphys.data)
                        {
                            if (d.images.original.url != null)
                                urlList.urlList.Add(d.images.original.url);
                        }
                }

                return urlList;
            }
            catch(Exception ex)
            {
                throw new Exception("Exception in takeOutURL cannot take out Giphys url", ex);
            }
        }
    }
}
