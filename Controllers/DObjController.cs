using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class DObjController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public DObjController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("get-dobj-data")]
    public IActionResult GetDObjData()
    {
        string? connectionString = _configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500, "Database connection string is not configured.");
        }

        string query = "SELECT * FROM \"dobj\""; // Adjust table name if needed

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var results = new List<Dictionary<string, object>>();

                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            object value = reader.GetValue(i);
                            row[reader.GetName(i)] = value is DBNull ? null : value; // Handle DBNull values
                        }
                        results.Add(row);
                    }
                    return Ok(results);
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error executing query: {ex.Message}");
        }
    }
}
