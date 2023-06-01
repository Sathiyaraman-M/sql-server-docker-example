using Microsoft.EntityFrameworkCore;

namespace SqlServerDockerExample.Data;

public class DepartmentService
{
    private readonly AppDbContext _appDbContext;

    public DepartmentService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<List<Department>> GetDepartmentsAsync()
    {
        return await _appDbContext.Departments.ToListAsync();
    }
    
    public async Task<Department> GetDepartmentByIdAsync(int id)
    {
        return await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
    }
    
    public async Task<Department> AddDepartmentAsync(Department department)
    {
        var result = await _appDbContext.Departments.AddAsync(department);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Department> UpdateDepartmentAsync(Department department)
    {
        var result = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);
        if (result == null) 
            return null;
        result.Name = department.Name;
        result.ShortName = department.ShortName;
        await _appDbContext.SaveChangesAsync();
        return result;
    }
    
    public async Task DeleteDepartmentAsync(int id)
    {
        var result = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
        if (result != null)
        {
            _appDbContext.Departments.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
    }
}