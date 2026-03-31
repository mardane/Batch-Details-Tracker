using System.Text.Json;
using BatchDetailsTracker.Web.Models;

namespace BatchDetailsTracker.Web.Services;

public class BatchTrackerService
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _syncLock = new(1, 1);
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public BatchTrackerService(IWebHostEnvironment environment)
    {
        var dataDirectory = Path.Combine(environment.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dataDirectory);
        _filePath = Path.Combine(dataDirectory, "batches.json");
    }

    public async Task<IReadOnlyList<BatchRecord>> GetBatchesAsync()
    {
        await _syncLock.WaitAsync();

        try
        {
            var batches = await ReadBatchesInternalAsync();
            var seeded = SeedIfEmpty(batches);

            if (seeded)
            {
                await SaveBatchesInternalAsync(batches);
            }

            return batches
                .OrderByDescending(batch => batch.BatchDate)
                .ThenByDescending(batch => batch.TotalEarnings)
                .ToList();
        }
        finally
        {
            _syncLock.Release();
        }
    }

    public async Task AddBatchAsync(BatchRecord batch)
    {
        ArgumentNullException.ThrowIfNull(batch);

        await _syncLock.WaitAsync();

        try
        {
            var batches = await ReadBatchesInternalAsync();
            SeedIfEmpty(batches);

            batch.Id = Guid.NewGuid();
            batch.BatchName = batch.BatchName.Trim();
            batch.StoreName = batch.StoreName.Trim();
            batch.Notes = batch.Notes.Trim();

            batches.Add(batch);
            await SaveBatchesInternalAsync(batches);
        }
        finally
        {
            _syncLock.Release();
        }
    }

    private async Task<List<BatchRecord>> ReadBatchesInternalAsync()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        var fileInfo = new FileInfo(_filePath);
        if (fileInfo.Length == 0)
        {
            return [];
        }

        await using var stream = File.OpenRead(_filePath);
        return await JsonSerializer.DeserializeAsync<List<BatchRecord>>(stream, _jsonOptions) ?? [];
    }

    private async Task SaveBatchesInternalAsync(List<BatchRecord> batches)
    {
        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, batches, _jsonOptions);
    }

    private static bool SeedIfEmpty(List<BatchRecord> batches)
    {
        if (batches.Count > 0)
        {
            return false;
        }

        var today = DateTime.Today;

        batches.AddRange(
        [
            new BatchRecord
            {
                BatchName = "Morning Costco Run",
                StoreName = "Costco",
                BatchDate = today,
                Status = BatchStatus.Completed,
                OrdersCount = 2,
                BasePay = 26.50m,
                TipAmount = 22.00m,
                Adjustments = 0,
                MilesDriven = 11.2,
                HoursSpent = 1.6,
                Notes = "Fast checkout and easy drop-off."
            },
            new BatchRecord
            {
                BatchName = "Afternoon Target Batch",
                StoreName = "Target",
                BatchDate = today.AddDays(-1),
                Status = BatchStatus.Completed,
                OrdersCount = 3,
                BasePay = 31.00m,
                TipAmount = 18.50m,
                Adjustments = 4.00m,
                MilesDriven = 13.4,
                HoursSpent = 2.1,
                Notes = "Included heavy items but paid well."
            },
            new BatchRecord
            {
                BatchName = "Evening Sprouts Pickup",
                StoreName = "Sprouts",
                BatchDate = today.AddDays(-2),
                Status = BatchStatus.Active,
                OrdersCount = 1,
                BasePay = 14.00m,
                TipAmount = 8.00m,
                Adjustments = 0,
                MilesDriven = 5.3,
                HoursSpent = 0.9,
                Notes = "Good short-distance batch."
            }
        ]);

        return true;
    }
}
