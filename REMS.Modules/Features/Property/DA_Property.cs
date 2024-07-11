﻿namespace REMS.Modules.Features.Property;

public class DA_Property
{
    private readonly AppDbContext _db;

    public DA_Property(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<PropertyResponseModel>> GetProperties()
    {
        try
        {
            var properties = await _db.Properties
                                      .AsNoTracking()
                                      .Include(x => x.PropertyImages)
                                      .ToListAsync();

            var response = properties.Select(property => new PropertyResponseModel
            {
                Property = property.Change(),
                Images = property.PropertyImages.Select(x => x.Change()).ToList()
            }).ToList();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public async Task<PropertyListResponseModel> GetProperties(int pageNo = 1, int pageSize = 10)
    {
        try
        {
            var properties = await _db.Properties
                                      .AsNoTracking()
                                      .Include(x=>x.PropertyImages)
                                      .Skip((pageNo - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            var propertyResponseModels = properties.Select(property => new PropertyResponseModel
            {
                Property = property.Change(),
                Images = property.PropertyImages.Select(x => x.Change()).ToList()
            }).ToList();

            var totalCount = await _db.Properties.CountAsync();
            var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

            var response = new PropertyListResponseModel
            {
                Properties = propertyResponseModels,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, totalCount)
            };

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public async Task<PropertyResponseModel> GetPropertyById(int propertyId)
    {
        try
        {
            var property = await _db.Properties
                                    .AsNoTracking()
                                    .Include(x=>x.PropertyImages)
                                    .FirstOrDefaultAsync(x => x.PropertyId == propertyId)
                                    ?? throw new Exception("Property Not Found");

            var responseModel = new PropertyResponseModel
            {
                Property = property.Change(),
                Images = property.PropertyImages.Select(x => x.Change()).ToList()
            };

            return responseModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}