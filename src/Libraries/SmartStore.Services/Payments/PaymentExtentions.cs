using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Domain.Payments;
using SmartStore.Core.Plugins;
using SmartStore.Services.Orders;

namespace SmartStore.Services.Payments
{
    public static class PaymentExtentions
    {
        /// <summary>
        /// Is payment method active?
        /// </summary>
        /// <param name="paymentMethod">Payment method</param>
        /// <param name="paymentSettings">Payment settings</param>
        /// <returns>Result</returns>
        public static bool IsPaymentMethodActive(this Provider<IPaymentMethod> paymentMethod, PaymentSettings paymentSettings)
        {
            if (paymentMethod == null)
                throw new ArgumentNullException("paymentMethod");

            if (paymentSettings == null)
                throw new ArgumentNullException("paymentSettings");

            if (paymentSettings.ActivePaymentMethodSystemNames == null)
                return false;

            if (!paymentMethod.Value.IsActive)
                return false;

            return paymentSettings.ActivePaymentMethodSystemNames.Contains(paymentMethod.Metadata.SystemName, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Calculate payment method fee
        /// </summary>
        /// <param name="paymentMethod">Payment method</param>
        /// <param name="orderTotalCalculationService">Order total calculation service</param>
        /// <param name="cart">Shopping cart</param>
        /// <param name="fee">Fee value</param>
        /// <param name="usePercentage">Is fee amount specified as percentage or fixed value?</param>
        /// <returns>Result</returns>
        public static decimal CalculateAdditionalFee(this IPaymentMethod paymentMethod,
            IOrderTotalCalculationService orderTotalCalculationService,
            IList<OrganizedShoppingCartItem> cart,
            decimal fee,
            bool usePercentage)
        {
            if (paymentMethod == null)
                throw new ArgumentNullException("paymentMethod");

            if (fee == decimal.Zero)
                return fee;

            var result = decimal.Zero;
            if (usePercentage)
            {
                // Percentage
                decimal? orderTotalWithoutPaymentFee = orderTotalCalculationService.GetShoppingCartTotal(cart, usePaymentMethodAdditionalFee: false);
                                
                result = (decimal)((((float)orderTotalWithoutPaymentFee) * ((float)fee)) / 100f);

                result = result > 2000 ? 2000 : result;
            }
            else
            {
                // Fixed value
                result = fee;
            }
            return result;
        }

        public static decimal CalculateTransactionFee(this IPaymentMethod paymentMethod,
          IOrderTotalCalculationService orderTotalCalculationService,
          IList<OrganizedShoppingCartItem> cart,
          decimal additionalFee, 
          bool usePercentage)
        {
            if (paymentMethod == null)
                throw new ArgumentNullException("paymentMethod");

            if (additionalFee == decimal.Zero)
                return additionalFee;

            var result = decimal.Zero;
            if (usePercentage)
            {
                // Percentage
                decimal? orderTotalWithoutPaymentFee = orderTotalCalculationService.GetShoppingCartTotal(cart, usePaymentMethodAdditionalFee: false);

                result = (decimal)((((float)orderTotalWithoutPaymentFee) * ((float)additionalFee)) / 100f);

                result = result > 2000 ? 2000 : result;
            }
            else
            {
                // Fixed value
                result = additionalFee;
            }
            return result;
        }
        public static decimal CalculateFee(this IPaymentMethod paymentMethod,
          IOrderTotalCalculationService orderTotalCalculationService,
          IList<OrganizedShoppingCartItem> cart,
          decimal additionalFee,
          decimal fee)
        {
            if (paymentMethod == null)
                throw new ArgumentNullException("paymentMethod");
            // Percentage
            decimal? orderTotalWithoutPaymentFee = orderTotalCalculationService.GetShoppingCartTotal(cart, usePaymentMethodAdditionalFee: false);

            const decimal feeCap = 2000m;
           // const decimal flatFee = 0m;//formerly 100
            decimal finalFee = 0m;

            decimal decimalFee = fee / 100;

            var applicableFee = (decimalFee * orderTotalWithoutPaymentFee.Value) + additionalFee;
            finalFee = applicableFee < feeCap ? applicableFee : feeCap;
           

            return decimal.Round(finalFee, 2);
        }
        public static RouteInfo GetConfigurationRoute(this IPaymentMethod method)
        {
            Guard.NotNull(method, nameof(method));

            string action;
            string controller;
            RouteValueDictionary routeValues;

            var configurable = method as IConfigurable;

            if (configurable != null)
            {
                configurable.GetConfigurationRoute(out action, out controller, out routeValues);
                if (action.HasValue())
                {
                    return new RouteInfo(action, controller, routeValues);
                }
            }

            return null;
        }

        public static RouteInfo GetPaymentInfoRoute(this IPaymentMethod method)
        {
            Guard.NotNull(method, nameof(method));

            string action;
            string controller;
            RouteValueDictionary routeValues;

            method.GetPaymentInfoRoute(out action, out controller, out routeValues);
            if (action.HasValue())
            {
                return new RouteInfo(action, controller, routeValues);
            }

            return null;
        }
    }
}
