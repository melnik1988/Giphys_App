using Giphy_App.Moduls.Responces;
using Giphys_App.Interface;
using Giphys_App.Moduls.Classes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Giphy_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiphyController : ControllerBase
    {
        private const string url = "https://api.giphy.com/v1/gifs/search?";
        private IConfiguration configuration;
        private IHelper helperService;
        private IFile fileService;
        private string Delimiter = ",";


        public GiphyController(IConfiguration _configuration, IHelper _helperService, IFile _fileService)
        {
            this.configuration = _configuration;
            this.helperService = _helperService;
            this.fileService = _fileService;
        }

        [HttpGet("{value}")]
        public async Task<string> SearchGiphy(string value)
        {
            var apiKey = configuration.GetSection("AppSettings:API_Key");
            List<GiphyURL> giphys_json = new List<GiphyURL>();

            string[] subs = value.Split(Delimiter);
            subs = subs.Distinct().ToArray();

            try
            {
                // Parallel
                var tasks = new List<Task>();
                foreach (var s in subs)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        await GetGiphys(apiKey, giphys_json, s);
                    }));
                }
                await Task.WhenAll(tasks);

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in SearchGiphy", ex);
            }

            return JsonConvert.SerializeObject(giphys_json);
        }

        private async Task GetGiphys(IConfigurationSection apiKey, List<GiphyURL> giphys_json, string str)
        {
            string full_url = $"{url}api_key={apiKey.Value}&q={str}&limit=25&offset=0&rating=g&lang=en";

            if (fileService.CheckIfFileExist(str))
            {
                string json = fileService.ReadFromFile(str);
                giphys_json.Add(JsonConvert.DeserializeObject<GiphyURL>(json));
            }
            else
            {

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(full_url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var giphys = JsonConvert.DeserializeObject<Giphys>(apiResponse);

                        var listUrl = helperService.takeOutURL(giphys, str);

                        string json = JsonConvert.SerializeObject(listUrl);

                        if (!fileService.CheckIfFileExist(str))
                        {
                            fileService.WriteToFile(json, str);
                        }

                        giphys_json.Add(listUrl);
                    }
                }

            }
        }

        [HttpGet("list/")]
        public ActionResult<string> Get()
        {
            return "the server work";
        }
    }
}
