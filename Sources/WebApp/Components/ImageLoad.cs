using Microsoft.AspNetCore.Mvc;
using WebApp.Settings;

namespace WebApp.Components
{
    public class ImageLoad : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int cabinetId) 
        {
            //string result = $"<p>Текущее время:<b>{DateTime.Now:HH:mm:ss}</b></p>";

            //return new HtmlContentViewComponentResult(new HtmlString(result));
            Console.WriteLine(cabinetId);

            //http://localhost:5215/api/Cabinet/image/getImagesByCab=
            var list = await AppStatics.ApiClient.GetFromJsonAsync<List<string>>("http://localhost:5215/api/Cabinet/image/getImagesByCab=" + cabinetId);

            return View("_ImageContainerPartial", list);
        }
    }
}
