using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;



var builder = WebApplication.CreateBuilder();
var app = builder.Build();

string apiKey = "451ee270d51333171b8966ed41db9f8e";
string url =  $"https://api.openweathermap.org/data/2.5/weather?q=";
string url2 = $"https://api.openweathermap.org/data/2.5/weather?id=";

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    if (context.Request.Path == "/showById")
    {

        var form = context.Request.Form;
        var cityId = form["cityId"];
        WebRequest request = WebRequest.Create(url2 + cityId + "&units=metric&appid=" + apiKey);

        request.Method = "POST";
        WebResponse response = await request.GetResponseAsync();
        string answer = string.Empty;
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            answer = await reader.ReadToEndAsync();
        }
        response.Close();

        WeatherResponse response_global = JsonConvert.DeserializeObject<WeatherResponse>(answer);
        await WriteResponseAsync(response_global, context);


    }
    else if(context.Request.Path == "/showByName")
    {
        var form = context.Request.Form;
        var cityName = form["cityName"];
        WebRequest request = WebRequest.Create(url + cityName + "&units=metric&appid=" + apiKey);
        request.Method = "POST";
        WebResponse response = await request.GetResponseAsync();
        string answer = string.Empty;
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            answer = await reader.ReadToEndAsync();
        }
        response.Close();

        WeatherResponse response_global = JsonConvert.DeserializeObject<WeatherResponse>(answer);
        await WriteResponseAsync(response_global, context);
    }
    else
    {
        await context.Response.SendFileAsync("html/index_weather.html");
    }
});
app.UseStaticFiles();
app.Run();

async Task WriteResponseAsync(WeatherResponse response_global, HttpContext context)
{
    await context.Response.WriteAsync($"<div>" +
            $"<p>Средняя температура в данный момент в городе " + response_global.name + " = " + response_global.main.temp +
            $"<br>Погода за окном: <br>" + response_global.weather[0].main + "<br>" + response_global.weather[0].description +
            $"<br>Ветер: " + response_global.wind.speed +"м/c" +
            $"</p></div>");
    Logger.Log($"Средняя температура в данный момент в городе {response_global.name} = {response_global.main.temp}" +
        $"\nПогода за окном: \n {response_global.weather[0].main} \n{response_global.weather[0].description}");
}

public class Logger
{
    public static void Log(string message)
    {
        string logPath = @"html\logs.txt";
        using (StreamWriter writer = new StreamWriter(logPath, true))
        {
            writer.WriteLine(string.Format("{0} {1}: {2}\n\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), message));
        }
    }
}

public class Temperatura
{
    public double temp;
    public double feels_like;
}

public class WeatherNow
{
    public string main;
    public string description;
}

public class Wind
{
    public string speed;
}

public class WeatherResponse
{
    public Temperatura main;
    public string name;
    public WeatherNow[] weather;
    public Wind wind;
}