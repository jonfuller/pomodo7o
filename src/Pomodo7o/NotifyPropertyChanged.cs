using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Pomodo7o
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(Expression<Func<T>> propExpr, Expression<Func<T>> fieldExpr, T value)
        {
            SetProperty(propExpr, fieldExpr, value, () => { });
        }

        protected void SetProperty<T>(Expression<Func<T>> propExpr, Expression<Func<T>> fieldExpr, T value, Action doIfChanged)
        {
            var prop = (PropertyInfo)((MemberExpression)propExpr.Body).Member;
            var field = (FieldInfo)((MemberExpression)fieldExpr.Body).Member;

            var currVal = prop.GetValue(this, null);

            if(currVal == null && value == null)
                return;
            if(currVal == null || !currVal.Equals(value))
            {
                field.SetValue(this, value);
                doIfChanged();

                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
            }
        }
    }
}