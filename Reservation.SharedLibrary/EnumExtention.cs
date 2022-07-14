using System;
using System.ComponentModel;

namespace Reservation.SharedLibrary
{
    public static class EnumExtention
    {
        /// <summary>
        /// EnumのValue(int)からDescriptionを取得します
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEmumDescriptionFromValue<T>(this T value) where T : Enum
        {
            var description = typeof(T).GetField(value.ToString())?                         //FiledInfoを取得
                                .GetCustomAttributes(typeof(DescriptionAttribute), false)   //DescriptionAttributeのリストを取得           
                                .Cast<DescriptionAttribute>()                               //DescriptionAttributeにキャスト
                                .FirstOrDefault()?                                          //最初の一つを取得、なければnull
                                .Description;                                               //DescriptionAttributeがあればDescriptionを、なければnullを返す

            return description ?? value.ToString();                                         //descriptionがnullならvalue.ToString()を返すs
        }


        /// <summary>
        /// EnumのDescriptionからEnumのValueを取得します
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T? GetEnumValueFromDescription<T>(this string description) where T: Enum
        {
            var value = typeof(T).GetFields()
                        .SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), false),
                            (f, a) => new { field = f, attribute = a })
                        .Where(x => ((DescriptionAttribute)x.attribute).Description == description)
                        .FirstOrDefault()?
                        .field.GetRawConstantValue();

            return (T?)(value ?? default(T));
        }

        /// <summary>
        /// int値からEnumのオブジェクト値に変換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetEnumValueFromInt<T>(this int value) where T: Enum
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}

