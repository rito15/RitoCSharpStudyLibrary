using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Rito
{
    // 2020. 03. 09. (월) 최초 작성

    /// <summary>
    /// <para/> 2020. 03. 09. 작성
    /// <para/> --------------------------------------------------------
    /// <para/> [메소드 목록]
    /// <para/> Ex_Rf_Get~ Members, Fields, Methods, Properties
    /// <para/> : 특정 조건의 멤버, 필드, 메소드, 또는 프로퍼티 정보 가져오기
    /// <para/> -> 조건 : public, non-public, instance, static
    /// <para/> 
    /// </summary>
    public static class ReflectionExtension
    {
        #region Get Members

        // 모든 멤버 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 멤버(필드, 메소드) 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 멤버들도 포함
        /// </summary>
        public static MemberInfo[] Ex_Rf_GetAllMembers(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMembers(flags);
        }

        // 모든 public 멤버 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 멤버(필드, 메소드) 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 멤버들도 포함
        /// </summary>
        public static MemberInfo[] Ex_Rf_GetAllPublicMembers(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMembers(flags);
        }

        // 모든 public instance 멤버 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 멤버(필드, 메소드) 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 멤버들도 포함
        /// </summary>
        public static MemberInfo[] Ex_Rf_GetPublicInstanceMembers(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMembers(flags);
        }

        // 모든 public static 멤버 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 멤버(필드, 메소드) 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 멤버들도 포함
        /// </summary>
        public static MemberInfo[] Ex_Rf_GetPublicStaticMembers(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMembers(flags);
        }

        // 모든 nonPublic 멤버 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 멤버(필드, 메소드) 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 멤버들도 포함
        /// </summary>
        public static MemberInfo[] Ex_Rf_GetAllNonPublicMembers(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMembers(flags);
        }

        // 모든 nonPublic instance 멤버 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 동적 멤버(필드, 메소드) 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 멤버들도 포함
        /// </summary>
        public static MemberInfo[] Ex_Rf_GetNonPublicInstanceMembers(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMembers(flags);
        }

        // 모든 nonPublic static 멤버 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 정적 멤버(필드, 메소드) 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 멤버들도 포함
        /// </summary>
        public static MemberInfo[] Ex_Rf_GetNonPublicStaticMembers(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMembers(flags);
        }

        #endregion // ==========================================================

        #region Get Methods

        // 모든 메소드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 메소드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 메소드들도 포함
        /// </summary>
        public static MethodInfo[] Ex_Rf_GetAllMethods(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMethods(flags);
        }

        // 모든 public 메소드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 메소드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 메소드들도 포함
        /// </summary>
        public static MethodInfo[] Ex_Rf_GetAllPublicMethods(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMethods(flags);
        }

        // 모든 public instance 메소드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 메소드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 메소드들도 포함
        /// </summary>
        public static MethodInfo[] Ex_Rf_GetPublicInstanceMethods(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMethods(flags);
        }

        // 모든 public static 메소드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 메소드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 메소드들도 포함
        /// </summary>
        public static MethodInfo[] Ex_Rf_GetPublicStaticMethods(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMethods(flags);
        }

        // 모든 nonPublic 메소드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 메소드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 메소드들도 포함
        /// </summary>
        public static MethodInfo[] Ex_Rf_GetAllNonPublicMethods(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMethods(flags);
        }

        // 모든 nonPublic instance 메소드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 동적 메소드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 메소드들도 포함
        /// </summary>
        public static MethodInfo[] Ex_Rf_GetNonPublicInstanceMethods(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMethods(flags);
        }

        // 모든 nonPublic static 메소드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 정적 메소드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 메소드들도 포함
        /// </summary>
        public static MethodInfo[] Ex_Rf_GetNonPublicStaticMethods(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetMethods(flags);
        }

        #endregion // ==========================================================

        #region Get Fields

        // 모든 필드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 필드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 필드들도 포함
        /// </summary>
        public static FieldInfo[] Ex_Rf_GetAllFields(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetFields(flags);
        }

        // 모든 public 필드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 필드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 필드들도 포함
        /// </summary>
        public static FieldInfo[] Ex_Rf_GetAllPublicFields(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetFields(flags);
        }

        // 모든 public instance 필드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 필드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 필드들도 포함
        /// </summary>
        public static FieldInfo[] Ex_Rf_GetPublicInstanceFields(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetFields(flags);
        }

        // 모든 public static 필드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 필드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 필드들도 포함
        /// </summary>
        public static FieldInfo[] Ex_Rf_GetPublicStaticFields(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetFields(flags);
        }

        // 모든 nonPublic 필드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 필드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 필드들도 포함
        /// </summary>
        public static FieldInfo[] Ex_Rf_GetAllNonPublicFields(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetFields(flags);
        }

        // 모든 nonPublic instance 필드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 동적 필드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 필드들도 포함
        /// </summary>
        public static FieldInfo[] Ex_Rf_GetNonPublicInstanceFields(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetFields(flags);
        }

        // 모든 nonPublic static 필드 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 정적 필드 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 필드들도 포함
        /// </summary>
        public static FieldInfo[] Ex_Rf_GetNonPublicStaticFields(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetFields(flags);
        }

        #endregion // ==========================================================

        #region Get Properties

        // 모든 프로퍼티 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 프로퍼티 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 프로퍼티들도 포함
        /// </summary>
        public static PropertyInfo[] Ex_Rf_GetAllProperties(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetProperties(flags);
        }

        // 모든 public 프로퍼티 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 프로퍼티 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 프로퍼티들도 포함
        /// </summary>
        public static PropertyInfo[] Ex_Rf_GetAllPublicProperties(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetProperties(flags);
        }

        // 모든 public instance 프로퍼티 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 프로퍼티 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 프로퍼티들도 포함
        /// </summary>
        public static PropertyInfo[] Ex_Rf_GetPublicInstanceProperties(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetProperties(flags);
        }

        // 모든 public static 프로퍼티 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 public 동적 프로퍼티 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 프로퍼티들도 포함
        /// </summary>
        public static PropertyInfo[] Ex_Rf_GetPublicStaticProperties(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetProperties(flags);
        }

        // 모든 nonPublic 프로퍼티 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 프로퍼티 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 프로퍼티들도 포함
        /// </summary>
        public static PropertyInfo[] Ex_Rf_GetAllNonPublicProperties(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetProperties(flags);
        }

        // 모든 nonPublic instance 프로퍼티 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 동적 프로퍼티 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 프로퍼티들도 포함
        /// </summary>
        public static PropertyInfo[] Ex_Rf_GetNonPublicInstanceProperties(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetProperties(flags);
        }

        // 모든 nonPublic static 프로퍼티 가져오기
        /// <summary>
        /// <para/> [리플렉션 확장]
        /// <para/> 해당 클래스의 모든 non-public 정적 프로퍼티 정보 리턴
        /// <para/> --------------------------------------------------
        /// <para/> [매개변수]
        /// <para/> containsParents : 부모의 프로퍼티들도 포함
        /// </summary>
        public static PropertyInfo[] Ex_Rf_GetNonPublicStaticProperties(this Type target, bool containsParents = false)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            if (!containsParents)
                flags |= BindingFlags.DeclaredOnly;

            return target.GetProperties(flags);
        }

        #endregion // ==========================================================
    }
}
