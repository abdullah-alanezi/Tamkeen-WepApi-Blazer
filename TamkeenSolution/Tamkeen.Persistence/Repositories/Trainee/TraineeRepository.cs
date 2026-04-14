using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Domain.Entities.Trainee; // تأكد من صحة مسار الـ Entity
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Trainee
{
    public class TraineeRepository : GenericRepository<Tamkeen.Domain.Entities.Trainee.Trainee>, ITraineeRepository
    {
        public TraineeRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        // 1. إضافة متدرب
        public async Task<bool> AddTraineeAsync(Tamkeen.Domain.Entities.Trainee.Trainee entity)
        {
            await AddAsync(entity);
            // إرجاع true إذا تم حفظ سجل واحد على الأقل
            return await SaveChangesAsync() > 0;
        }

        // 2. تحديث متدرب
        public async Task<bool> UpdateTraineeAsync(Tamkeen.Domain.Entities.Trainee.Trainee entity)
        {
            await UpdateAsync(entity);
            return await SaveChangesAsync() > 0;
        }

        // 3. حذف متدرب باستخدام الـ Guid
        public async Task<bool> DeleteTraineeAsync(Guid id)
        {
            // البحث عن العنصر أولاً للتأكد من وجوده
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return false;

            await DeleteAsync(entity);
            return await SaveChangesAsync() > 0;
        }

        // 4. جلب متدرب واحد بواسطة المعرف
        public async Task<Tamkeen.Domain.Entities.Trainee.Trainee?> GetTraineeByIdAsync(Guid id)
        {
            // نستخدم دالة الـ Generic للبحث بالشرط
            return await FirstOrDefaultAsync(x => x.Id == id);
        }

        // 5. جلب جميع المتدربين كقائمة للقراءة فقط
        public async Task<List<Tamkeen.Domain.Entities.Trainee.Trainee>> GetAllTraineesAsync()
        {
            
            return await GetAllAsync<Tamkeen.Domain.Entities.Trainee.Trainee>(whereCondition:null);
        }
    }
}