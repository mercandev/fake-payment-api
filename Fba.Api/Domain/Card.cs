using System;
using System.ComponentModel;
using Fba.Api.Const;
using Fba.Api.Exceptions;
using Fba.Api.Helper;
using Microsoft.VisualBasic;

namespace Fba.Api.Domain
{
	public class Card
	{
        [CustomFbaValidation]
        public string CustomerName { get; set; }

        [CustomFbaValidation]
        public string CardNumber { get; set; }

        [CustomFbaValidation]
        public int LastUseMount { get; set; }

        [CustomFbaValidation]
        public int LastUseYear { get; set; }

        [CustomFbaValidation]
        public int Cvv { get; set; }

        public CardPaymentType CardPaymentType { get; set; }

        public CardType CardType { get; set; }


        public Card(string customerName , string cardNumber , int lastUseMount , int lastUseYear , int cvv)
        {
            Contract.IsRequired((cardNumber.Length < 16 || cardNumber.Length > 16), ErrorConst.CARDNUMBER_ERROR);
            Contract.IsRequired((cvv < 0 || cvv > 3), ErrorConst.CARDCVV_ERROR);

            CustomerName = customerName;
            CardNumber = cardNumber;
            LastUseMount = lastUseMount;
            LastUseYear = lastUseYear;
        }

        public void CheckCardLastUseYears(int lastUseYear)
        {
            var currentYear = DateTime.Now.Year.ToString().Substring(3,4);

            if (!lastUseYear.ToString().Equals(currentYear)) throw new HbaBusinessException(ErrorConst.LASTUSEYEAR_ERROR);
        }

        public void CheckCardLastUseDate(int lastUseDate)
        {
            if (!Enumerable.Range(1, 31).Contains(lastUseDate)) throw new HbaBusinessException(ErrorConst.LASTUSEMOUNT_ERROR);
        }

        public void DelegateCardPaymentType()
        {
            var firstFourDigit = CardNumber.Substring(0, 3);

            if (CardPaymentTypeConst.MASTERCARD.Contains(firstFourDigit))
            {
                CardPaymentType = CardPaymentType.MasterCard;
            }

            if (CardPaymentTypeConst.VISA.Contains(firstFourDigit))
            {
                CardPaymentType = CardPaymentType.Visa;
            }

            if (CardPaymentTypeConst.TROY.Contains(firstFourDigit))
            {
                CardPaymentType = CardPaymentType.Troy;
            }

            throw new HbaBusinessException(ErrorConst.CARDPAYMENTTYPE_ERROR);
        }

        public void RandomDelegateCardType()
        {
            var randomPaymentType = System.Enum.GetValues<CardType>();

            CardType = (CardType)randomPaymentType.GetValue(new Random().Next(randomPaymentType.Length));
        }
    }

    public enum CardPaymentType
    {
        [Description("MasterCard")]
        MasterCard = 1,
        [Description("Visa")]
        Visa = 2,
        [Description("Troy")]
        Troy = 3
    }

    public enum CardType
    {
        [Description("Debit Card")]
        DebitCard = 1,
        [Description("Credit Card")]
        CreditCard = 2
    }
}

