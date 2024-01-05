using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static HttpClient client = new HttpClient();

    static async Task Main()
    {

        // Construct the API endpoint URL with the gridpoint for SF
        string apiUrl = $"https://api.weather.gov/gridpoints/MTR/85,105/forecast";

        // Set the necessary headers for the API request
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ld+json"));
        client.DefaultRequestHeaders.Add("User-Agent", "C# Console App");

        // Send the API request and get the response
        HttpResponseMessage response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)

        {
            // If the response is successful, retrieve the weather forecast data from the response content
            string jsonResponse = await response.Content.ReadAsStringAsync();

            // create variable with converted data 
            dynamic data = JsonConvert.DeserializeObject(jsonResponse);
            Console.WriteLine("In Sanfracisco the forcast is: \n");

            //parse data for relevant information and print to screen
            foreach (dynamic period in data.periods)
            {
                string short_forcast = period.shortForecast;
                string name = period.name;
                float temperature = period.temperature;
                string temperatureUnit = period.temperatureUnit;
                Console.WriteLine($"{short_forcast} {name} \n {temperature} {temperatureUnit}");
            }
        }
        else
        {
            // If the response is not successful, print the response status code and reason phrase
            Console.WriteLine($"Error: {response.StatusCode} {response.ReasonPhrase}");
        }

        // Wait for user input before closing the console window
        Console.ReadLine();
    }
}