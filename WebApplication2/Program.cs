var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    if (context.Request.Path == "/problem1")
    {
        var form = context.Request.Form;
        //string a = form["A"];
        //string b = form["B"];
        //string c = form["C"];

        var a = int.Parse(form["A"]);
        var b = int.Parse(form["B"]);
        var c = int.Parse(form["C"]);
        await context.Response.WriteAsync($"<div><p>�����������: {Math.Min(Math.Min(a, b), Math.Min(b, c))}</p>" +
            $"<p>������������: {Math.Max(Math.Max(a, b), Math.Max(b, c))}</p></div>");
        Logger.Log($"(1)������� ������: {a}, {b}, {c} \n�������� ������:\n�����������: {Math.Min(Math.Min(a, b), Math.Min(b, c))}\r\n������������: {Math.Max(Math.Max(a, b), Math.Max(b, c))}");
    }
    else if(context.Request.Path == "/problem2")
    {
        var form = context.Request.Form;
        //string a = form["A"];
        //string b = form["B"];
        //string c = form["C"];

        var y = int.Parse(form["year"]);
        string vis = y % 4 == 0?"����������":"������������";
        var cent = y / 100;
        if (y % 100 != 0) cent++; 
        await context.Response.WriteAsync($"<div><p>��� {y} {vis}</p>" +
            $"<p>���: {cent}</p></div>");
        Logger.Log($"(2)������� ������: {y} ��� \n�������� ������:\n��� {y} {vis}\n���: {cent}");
    }
    else
    {
        await context.Response.SendFileAsync("html/index_problem1.html");
    }
});

app.Run();

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