using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Common.Utils {

    #region Lambda拓展

    /// <summary>
    /// Lambda帮助类
    /// </summary>
    public static class LambdaUtils {

        /*
         * 使用方法：
         * 
         * Expression<Func<T, bool>> where = null;
         * Expression<Func<T, bool>> temp = null;
         * 
         * if (string.IsNullOrEmpty(str)) {
         *      temp = p => p.str == str;
         *      where = where.And(temp);
         *  }
         *  if (string.IsNullOrEmpty(code)) {
         *       temp = p => p.code == code;    
         *       where = where.Or(temp);    
         *   }
         *   
         *   if (where == null) 
         *        where = p => true;
         *   
         *   var list=DB.TableName.Where(where).ToList();
         */

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge) {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// 同"&&"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) {
            if (first == null)
                return second;
            if (second == null)
                return first;
            return first.Compose(second, Expression.And);
        }

        /// <summary>
        /// 同"||"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) {
            if (first == null)
                return second;
            if (second == null)
                return first;
            return first.Compose(second, Expression.Or);
        }
    }
    public class ParameterRebinder : ExpressionVisitor {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map) {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp) {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p) {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement)) {
                p = replacement;
            }
            return base.VisitParameter(p);
        }

    }

    #endregion

}
