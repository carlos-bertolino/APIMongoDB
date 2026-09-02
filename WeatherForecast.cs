namespace APIMongoDB;
/// <summary>
/// Representa os dados de uma previsão do tempo.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// A data em que a previsão foi calculada.
    /// </summary>
    /// <example>2026-09-02</example>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Temperatura medida em graus Celsius.
    /// </summary>
    /// <example>25</example>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperatura convertida automaticamente para Fahrenheit.
    /// </summary>
    /// <example>77</example>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Descrição textual sobre a sensação do clima.
    /// </summary>
    /// <example>Ensolarado</example>
    public string? Summary { get; set; }
}

