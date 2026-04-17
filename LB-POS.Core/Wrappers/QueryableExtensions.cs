using Microsoft.EntityFrameworkCore;

namespace LB_POS.Core.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
     this IQueryable<T> source,
     int pageNumber,
     int pageSize,
     CancellationToken cancellationToken = default) // 👈 إضافة CancellationToken
     where T : class
        {
            // 1. استخدام استثناء مخصص وواضح
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "The source IQueryable cannot be null.");
            }

            // 2. توحيد شروط التحقق في سطر واحد لكل متغير
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize; // تجنب الأرقام السالبة في حجم الصفحة أيضاً

            // 3. حساب العدد الكلي (تم تمرير الـ CancellationToken)
            int count = await source.CountAsync(cancellationToken);

            // خروج مبكر ممتاز لتقليل العبء على الداتا بيز
            if (count == 0)
            {
                return PaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);
            }

            // 4. جلب البيانات (تم تمرير الـ CancellationToken)
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
