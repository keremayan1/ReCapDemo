using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
   public class ValidationAspect:MethodInterception
   {
       private Type _type;

       public ValidationAspect(Type type)
       {
           if (!typeof(IValidator).IsAssignableFrom(type))
           {
               throw new Exception("Sistem hatalı");
           }
           _type = type;
       }


       protected override void OnBefore(IInvocation invocation)
       {
           var validator =(IValidator)Activator.CreateInstance(_type);
           var entityType = _type.BaseType.GetGenericArguments()[0];
           var entities = invocation.Arguments.Where(p => p.GetType()==entityType);
           foreach (var entity in entities)
           {
               ValidationTool.Validate(validator,entity);
           }

       }
    }
}
