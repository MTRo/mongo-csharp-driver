/* Copyright 2010-2015 MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDB.Driver.Sync
{
    internal class AsyncCursorEnumerableAdapter<TOutput> : IEnumerable<TOutput>
    {
        private readonly Func<CancellationToken, IAsyncCursor<TOutput>> _executor;
        private readonly CancellationToken _cancellationToken;

        public AsyncCursorEnumerableAdapter(Func<CancellationToken, IAsyncCursor<TOutput>> executor, CancellationToken cancellationToken)
        {
            _executor = executor;
            _cancellationToken = cancellationToken;
        }

        public IEnumerator<TOutput> GetEnumerator()
        {
            var cursor = _executor(_cancellationToken);
            return new AsyncCursorEnumeratorAdapter<TOutput>(cursor, _cancellationToken).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
