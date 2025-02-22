﻿/**
 * Slice a 1-dimensional array into chunks of arrays in a 2-dimensional array
 * @param {Array<T>} arr - the input array
 * @param {number} chunkSize - desired chunk size
 * @returns {Array<Array<T>>} 2D array consisting chunks of the original array
 */
export const sliceIntoChunks = <T>(arr: T[], chunkSize: number): T[][] => {
  const res: T[][] = [];
  for (let i = 0; i < arr.length; i += chunkSize) {
    const chunk = arr.slice(i, i + chunkSize);
    res.push(chunk);
  }
  return res;
};

export const splitIntoNArrays = <T>(arr: T[], n: number): T[][] => {
  if (n <= 1) return [arr];

  const res: T[][] = [];
  const chunkSize = arr.length / n;

  for (let i = 0; i < arr.length; i += chunkSize) {
    const chunk = arr.slice(i, i + chunkSize);
    res.push(chunk);
  }

  return res;
};

/**
 * Pause execution for specified duration
 * @param {number} ms - sleep duration in milliseconds
 * @returns {Promise<unknown>} a Promise that resolves in the given duration
 */
export const sleep = (ms: number) =>
  new Promise((resolve) => setTimeout(resolve, ms));

/**
 * Retrieve the state injected by .NET
 * @param {string} id - the hidden field's id
 * @returns {T} a typed state object
 */
export const getStateFromBackend = <T>(id: string) =>
  JSON.parse((<HTMLInputElement>document.getElementById(id)).value) as T;

/**
 * Trigger ASP.NET server controls by simulating click
 * @param {Event} e - the event object triggered by v-on events
 * @param {string} id - the id of the control to be invoked
 * @param {string} val - the value to be passed into the control
 */
export const triggerBackendControl = (
  e: Event | undefined,
  id: string,
  val: string = ""
) => {
  const ctrl = document.getElementById(id) as HTMLInputElement;

  if (val !== "") {
    ctrl.value = val;
  }

  if (ctrl) {
    ctrl.click();
  }
};

// /**
//  * Trigger ASP.NET server controls by simulating click
//  * @param {Event} e - the event object triggered by v-on events
//  * @param {string} id - the id of the control's to be invoked
//  * @param {any} id - the id of the current element
//  */
// export const triggerBackendControlWithCurrentElement = (e: Event, id: string, arg: any) => {
//   var ctrl = document.getElementById(id);

//   if(ctrl != null) {
//     ctrl.
//   }
// }
