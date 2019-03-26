using System;

namespace Common
{
    public static class ResultExtensions
    {
        public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, TNewValue> func) where TError : class
        {
            if (result.IsFailure)
                return Result.Fail<TNewValue, TError>(result.Error);

            return Result.Ok<TNewValue, TError>(func(result.Value));
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, Result<TNewValue, TError>> func) where TError : class
        {
            if (result.IsFailure)
                return Result.Fail<TNewValue, TError>(result.Error);

            return func(result.Value);
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func(result.Value);
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return func();
        }

        public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<Result<TNewValue, TError>> func) where TError : class
        {
            if (result.IsFailure)
                return Result.Fail<TNewValue, TError>(result.Error);

            return func();
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func();
        }

        public static Result<TNewValue> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, Result<TNewValue>> func) where TError : class
        {
            if (result.IsFailure)
                return Result.Fail<TNewValue, TError>(result.Error);

            return func(result.Value);
        }

        public static Result OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, Result> func) where TError : class
        {
            if (result.IsFailure)
                return Result.Fail<TNewValue, TError>(result.Error);

            return func(result.Value);
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            return func(result.Value);
        }

        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static Result<TValue, TError> Ensure<TValue, TError>(this Result<TValue, TError> result,
            Func<TValue, bool> predicate, TError errorObject) where TError : class
        {
            if (result.IsFailure)
                return Result.Fail<TValue, TError>(result.Error);

            if (!predicate(result.Value))
                return Result.Fail<TValue, TError>(errorObject);

            return Result.Ok<TValue, TError>(result.Value);
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return Result.Ok(result.Value);
        }

        public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            if (!predicate())
                return Result.Fail(errorMessage);

            return Result.Ok();
        }

        public static Result<TNewValue, TError> Map<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, TNewValue> func) where TError : class
        {
            if (result.IsFailure)
                return Result.Fail<TNewValue, TError>(result.Error);

            return Result.Ok<TNewValue, TError>(func(result.Value));
        }

        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<T> Map<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        public static Result<TValue, TError> OnSuccess<TValue, TError>(this Result<TValue, TError> result,
            Action<TValue> action) where TError : class
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        public static K OnBoth<T, K>(this Result<T> result, Func<Result<T>, K> func)
        {
            return func(result);
        }

        public static TValue OnBoth<TValue, TError>(this Result<TValue, TError> result,
            Func<Result<TValue, TError>, TValue> func) where TError : class
        {
            return func(result);
        }

        public static T OnBoth<T>(
            this Result result,
            Func<T> onSuccessProvider,
            Func<string, T> onErrorProvider)
        {
            if (result.IsSuccess)
            {
                return onSuccessProvider();
            }

            return onErrorProvider(result.Error);
        }

        public static K OnBoth<T, K>(
            this Result<T> result,
            Func<T, K> onSuccessProvider,
            Func<string, K> onErrorProvider)
        {
            if (result.IsSuccess)
            {
                return onSuccessProvider(result.Value);
            }

            return onErrorProvider(result.Error);
        }

        public static Result<TValue, TError> OnFailure<TValue, TError>(this Result<TValue, TError> result,
            Action action) where TError : class
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result<TValue, TError> OnFailure<TValue, TError>(this Result<TValue, TError> result,
            Action<TError> action) where TError : class
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result<T> ToOkResult<T>(this T t) => Result.Ok(t);

        public static Result<K> ToOkResult<T, K>(this T t, Func<T, K> transformFunc) => Result.Ok(transformFunc(t));

        public static Result<T> ToFailResult<T>(this T t, string error) => Result.Fail<T>(error);

        public static Result<T> ToTypedResult<T>(this Result result, T t) => result.IsSuccess.OnBoth(
            () => Result.Ok(t),
            () => Result.Fail<T>(result.Error));

        public static Result<TK> ToTypedResult<T, TK>(this Result<T> result, TK tk) => result.IsSuccess.OnBoth(
            () => Result.Ok(tk),
            () => Result.Fail<TK>(result.Error));

        public static Maybe<string> ToMaybeError(this Result result)
        {
            if (result.IsFailure)
            {
                return result.Error;
            }

            return Maybe<string>.None;
        }
    }
}