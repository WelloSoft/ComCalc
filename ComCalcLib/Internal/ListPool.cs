using System;
using System.Collections.Generic;


namespace ComCalcLib
{
	
	/*This one is used for getting a ExtendedList class
	public static class ListPool<T>
	{
		// Object pool to avoid allocations.
		static readonly Stack<ExtendedList<T>> s_Stack = new Stack<ExtendedList<T>> ();

		/// Get a new list instance
		/// Replacement for new ExtendedList<T>()
		public static ExtendedList<T> Get ()
		{
			if (s_Stack.Count == 0)
				return new ExtendedList<T> ();
			else
				return s_Stack.Pop();
		}

		static bool isThisFlushable = typeof(T).Equals(typeof(IFlushable));
		/// Releasing this list with its children if possible
		public static void Release (ExtendedList<T> toRelease)
		{
			if (isThisFlushable) {
				for (int i = 0; i < toRelease.Count; i++) {
					var obj = (IFlushable)toRelease [i];
					obj.Flush ();
				}
			}
			toRelease.Clear ();
			s_Stack.Push (toRelease);
		}

		/// Releasing this list without flushing the childs
		/// used if reference child is still used somewhere
		public static void ReleaseNoFlush (ExtendedList<T> toRelease)
		{
			toRelease.Clear ();
			s_Stack.Push (toRelease);
		}
	}

	//This one is used for getting a ExtendedList class
	public static class ExListPool<T>
	{
		// Object pool to avoid allocations.
		static readonly Stack<ExtendedList<T>> s_Stack = new Stack<ExtendedList<T>> ();

		/// Get a new list instance
		/// Replacement for new ExtendedList<T>()
		public static ExtendedList<T> Get ()
		{
			if (s_Stack.Count == 0)
				return new ExtendedList<T> ();
			else
				return s_Stack.Pop();
		}

		public static ExtendedList<T> Get (int capacity)
		{
			if (s_Stack.Count == 0)
				return new ExtendedList<T> (capacity);
			else
				return s_Stack.Pop();
		}


		static bool isThisFlushable = typeof(T).Equals(typeof(IFlushable));
		/// Releasing this list with its children if possible
		public static void Release (ExtendedList<T> toRelease)
		{
			if (isThisFlushable) {
				for (int i = 0; i < toRelease.Count; i++) {
					var obj = (IFlushable)toRelease [i];
					obj.Flush ();
				}
			}
			toRelease.Clear ();
			s_Stack.Push (toRelease);
		}

		/// Releasing this list without flushing the childs
		/// used if reference child is still used somewhere
		public static void ReleaseNoFlush (ExtendedList<T> toRelease)
		{
			toRelease.Clear ();
			s_Stack.Push (toRelease);
		}
	}
	*/
	internal static class ObjPool<T> where T : class, IFlushable, new()
	{
		// Object pool to avoid allocations.
		private static readonly Stack<T> s_Stack = new Stack<T> ();

		public static T Get ()
		{
            
			T obj;
			if (s_Stack.Count == 0)
				obj = new T ();
			else
				obj = s_Stack.Pop();
			obj.IsFlushed = false;
			return obj;
		}

		public static void Release (T toRelease)
		{
			if (toRelease.IsFlushed)
				return;
			toRelease.IsFlushed = true;
			s_Stack.Push (toRelease);
		}
	}

	//Interface to get a class to be flushable (flush means to be released to the main class stack
	//when it's unused, later if code need a new instance, the main stack will give this class back
	//instead of creating a new instance (which later introducing Memory Garbages)).
	public interface IFlushable
	{
		bool IsFlushed { get; set; }

		void Flush ();
	}
	
	/* Example of Implementation: (Copy snippet code below as Template)
	
	using TexDrawLib;
	public class SomeClass : IFlushable 
	{
		
		///This class is replacement for New()
		public static SomeClass Get()
		{
			var obj = ObjPool<SomeClass>.Get();
			//Set up some stuff here
			return obj;
		}
		
		///used Internally, check whether it's already released or not
		///Public for convience, you shouldn't set this manually
		bool m_flushed = false;
		public bool IsFlushed { get { return m_flushed; } set { m_flushed = value; } }
		
		//Call this in your code if this class is in no longer use
		public void Flush()
		{
			//Reset additional stuff, properties, variables, all have to be set to it's default value.
			//then you can ...
			ObjPool<SomeClass>.Release(this);
		}
	}
	
	*/
}