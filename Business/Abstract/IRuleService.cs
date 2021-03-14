using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IRuleService<T>
   {
       void minCarName(T entity);
       void minDailyPrice(T entity);
   }
}
