/**
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

/**
 * Pause execution for specified duration 
 * @param {number} ms - sleep duration in milliseconds
 * @returns {Prmoise<unknown>} a Promise that resolves in the given duration
 */
export const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));